using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SETUNA.Main;
using ZXing;

namespace SETUNA
{
    public class Barcode
    {
        public static bool automate = true;
        public static Barcode INSTANCE = null;
        public static bool allowDUP = false;

        public static Barcode Create() => new Barcode();
        public static int Countdown = 0;
        public static int MaxRetry = 8;
        public int retry = MaxRetry;
        public int _Countdown = Countdown;
        public bool repair;
        public bool reload;
        int lastNo;

        public Dictionary<string, int> Parts = new Dictionary<string, int>();
        Dictionary<string, int> FailedList= new Dictionary<string, int>();
        readonly string[] history;
        List<string> BaseFile = new List<string>();

        string last = null;
        public readonly string Filename;
        public int PartNo = 0;
        string state = "loading";
        bool FirstMiss = false;

        delegate void _StartCapture();
        delegate ListBox _Worklist(ListBox listBox, object[] o);

        private Barcode()
        {
            Filename = "" + Countdown + "_" + DateTimeOffset.Now.ToUnixTimeMilliseconds();
            history = new string[Countdown + 1];
            ReAutoMate();
            reload = false;
        }

        public Barcode(string Filename)
        {
            this.Filename = Filename;

            var s = Filename.Split('_');
            int.TryParse(s[0], out Countdown);
            history = new string[Countdown + 1];

            for (var i_rerty = MaxRetry; i_rerty >= 0; i_rerty--)
            {
                retry = i_rerty;
                for (var i = 0; i <= Countdown; i++)
                {
                    PartNo = i;
                    var part = Path.ChangeExtension(GetPartFileName(), "base");
                    var jpg = GetJpgFileName();
                    Console.WriteLine("===BIG===" + part);
                    if (File.Exists(FullPath(part)))
                    {
                        Parts[part] = i;
                        var bs = File.ReadAllBytes(FullPath(part));
                        last = Convert.ToBase64String(bs);
                        history[i] = last;
                    }
                    else if (File.Exists(FullPath(jpg)))
                    {
                        FailedList[jpg] = i;
                    }
                }
            }

            ReAutoMate();
            reload = false;
            PartNo = 0;

            CheckList();
        }

        public void ReAutoMate()
        {
            last = null;
            repair = false;
            _Countdown = Countdown;
            PartNo = 0;
            retry = MaxRetry;
            automate = true;
            INSTANCE = this;
            reload = true;
            state = "inited";
            FirstMiss = false;
            lastNo = int.MinValue;
            if (BaseFile.Count > 0 || Parts.Count == 0 || !allowDUP)
            {
                return;
            }
            BaseFile.AddRange(Parts.Keys);
        }

        public static string FullPath(string f) => Path.Combine(Path.GetTempPath(), f);

        public bool Detect(Bitmap bmp)
        {
            var f = GetJpgFileName();
            if (retry-- < 0)
            {
                retry = MaxRetry;
                PartNo++;
                _Countdown--;
                return true;
            }

            if (reload)
            {
                //if (PartNo > 0)
                //{
                //    last = history[PartNo - 1];
                //}
                _Countdown = Countdown - PartNo;
            }

            _Countdown--;
            try
            {
                Console.WriteLine($"==BIG=={PartNo}/{_Countdown}/{Countdown}/{retry}");
                // create a barcode reader instance
                var reader = new BarcodeReader() { AutoRotate = false };

                var ratio = 1.5 + (MaxRetry - retry) / 10;
                // detect and decode the barcode inside the bitmap
                var size = (int)Math.Max(bmp.Width * ratio, bmp.Height * ratio);
                var qrcode = BitmapUtils.ScaleToSize(bmp, size, size);

                var result = reader.Decode(qrcode);
                if (result == null)
                {
                    state = "==BIG==Detect Null: " + PartNo;
                    if (reload && Parts.ContainsValue(PartNo) && PartNo > 0)
                    {
                        if (PartNo - lastNo > 1)
                        {
                            Thread.Sleep(Math.Min(5, PartNo - lastNo) * 5000);
                            lastNo = PartNo;
                        }
                        else if (lastNo > PartNo)
                        {
                            PartNo = lastNo;
                        }
                        state = "==BIG==Skip Null: " + PartNo;
                        PartNo++;
                        return true;
                    }
                    FirstMiss = true;
                    Console.WriteLine("==BIG==NonQR!");
                    var jpg = FullPath(f);
                    if (File.Exists(jpg))
                    {
                        File.Delete(jpg);
                    }
                    bmp.Save(FullPath(jpg), System.Drawing.Imaging.ImageFormat.Jpeg);
                    FailedList[f] = PartNo;
                    if (PartNo <= 0)
                    {
                        _Countdown = -1;
                        retry = -1;
                        return false;
                    }
                }
                else if (result.Text.Equals(last)|| (PartNo>1&&result.Text.Equals(history[PartNo-1])))
                {
                    state = "==BIG==Equals Last form:" + PartNo;
                    if (reload && retry < MaxRetry / 2)
                    {
                        LeftMouseClick();
                        if (lastNo == PartNo - 1)
                        {
                            Thread.Sleep(5000);
                        }
                        state = "==BIG==Pass Next from: " + PartNo;
                    }
                    Console.WriteLine("==BIG==Same! " + last);
                    Thread.Sleep(1000);
                    return false;
                }
                else if (result.Text.Length > 0 && !result.Text.Equals(last))
                {
                    state = "==BIG==QR" + PartNo;
                    lastNo = PartNo;

                    var txt = result.Text;
                    Console.WriteLine("==BIG==QR! " + txt);

                    //if (reload && history[PartNo] != null && !txt.Equals(history[PartNo]))
                    //{
                    //    //重新定位 暂时废弃
                    //    //Console.WriteLine("==BIG==reloadQR confirmed!" + PartNo);
                    //    var num = FindHistory(txt, PartNo);

                    //    for (var i = 0; i < num; i++)
                    //    {
                    //        if (history[i] == null)
                    //        {
                    //            FirstMiss = true;
                    //            break;
                    //        }
                    //    }

                    //    //if (lastNo == PartNo - 1 && last != null && last.Equals(history[PartNo - 1]))
                    //    //{
                    //        //history[PartNo] = txt;
                    //        //state = "==BIG==Fix Relocate: " + PartNo;
                    //        //CreatePart(txt);
                    //        //Console.WriteLine("==BIG==addQR!! " + PartNo);
                    //        //lastNo = int.MinValue;
                    //    //}
                    //    //else
                    //    //{
                    //        //if (FirstMiss)
                    //        //{
                    //        //    num = Math.Max(num, PartNo);
                    //        //}

                    //        //state = "==BIG==Reloacte: " + PartNo + "=>" + num;
                    //        //PartNo = num;

                    //        //PartNo++;
                    //    //}
                    //    lastNo = int.MinValue;
                    //    PartNo = Math.Max(num, PartNo);
                    //    PartNo++;

                    //}
                    //else if (reload && history[PartNo] == null)
                    //{
                    //    if (history[PartNo - 1] != null && last != null && history[PartNo - 1].Equals(last))
                    //    {
                    //        history[PartNo] = txt;
                    //        state = "==BIG==Fix: " + PartNo;
                    //        CreatePart(txt);
                    //        Console.WriteLine("==BIG==addQR!! " + PartNo);
                    //        lastNo = int.MinValue;
                    //    }
                    //    else
                    //    {
                    //        state = "==BIG==Skip Fix: " + PartNo;
                    //        PartNo++;
                    //    }
                    //}
                    if (!reload)
                    {
                        state = "==BIG==addQR! " + PartNo;
                        Console.WriteLine(state);
                        history[PartNo] = txt;
                        CreatePart(txt);
                    }
                    else if (reload && history[PartNo] == null && PartNo > 0 && history[PartNo - 1] != null && last != null && history[PartNo - 1].Equals(last))
                    {
                        state = "==BIG==addQR!! " + PartNo;
                        Console.WriteLine(state);
                        history[PartNo] = txt;
                        CreatePart(txt);
                    }
                    else if (!txt.Equals(history[PartNo]))
                    {
                        var num = FindHistory(txt, PartNo);
                        state = $"==BIG==goto {PartNo}<===>{num}";
                        Console.WriteLine(state);
                        PartNo = Math.Max(num, PartNo) + 1;
                    }
                    else
                    {
                        state = $"==BIG==DoNothing {PartNo}";
                        Console.WriteLine(state);
                        PartNo++;
                    }
                    last = txt;
                    retry = MaxRetry;

                    return true;
                }
                else
                {
                    state = "==BIG==Detect Error: " + PartNo;
                }

                _Countdown++;
                Console.WriteLine("==BIG==QR End!");
            }
            catch (Exception e)
            {
                Console.WriteLine("==BIG==QR err: " + e.Message);
            }

            return false;
        }

        int FindHistory(string txt, int defaultNo)
        {
            var start = Array.IndexOf(history, txt);
            var end = Array.LastIndexOf(history, txt);

            if (start < 0)
            {
                return default;
            }
            else if (start == end)
            {
                return start;
            }

            start = Array.LastIndexOf(history, txt, defaultNo);
            if (start > 0)
            {
                return start;
            }

            end = Array.IndexOf(history, txt, defaultNo);
            if (end > 0)
            {
                return end;
            }

            return defaultNo;
        }

        string GetBaseFileName(int num) => Filename + "_" + num;
        string GetJpgFileName() => Path.ChangeExtension(GetBaseFileName(PartNo) + "_" + retry, "jpg");
        string GetPartFileName() => Path.ChangeExtension(GetBaseFileName(PartNo) + "_" + retry, "part");

        string CreatePart(string text)
        {
            var f = GetPartFileName();
            var msg = Encoding.UTF8.GetBytes(text);
            try
            {
                msg = Convert.FromBase64String(text);
                f = Path.ChangeExtension(f, "base");
                Parts[f] = PartNo;
            }
            catch (Exception e)
            {
                //_Countdown++;
                FailedList[f] = PartNo;
                Console.WriteLine("==BIG==" + e.Message);
            }
            var save = FullPath(f);

            if (!BaseFile.Contains(f))
            {
                if (File.Exists(save))
                {
                    File.Delete(save);
                }
                File.WriteAllBytes(save, msg);
            }
            else
            {
                state = "===BIG===BaseFile ReadOnly: " + f;
                Console.WriteLine(state);
                history[PartNo] = Convert.ToBase64String(File.ReadAllBytes(save));
            }

            PartNo++;
            Console.WriteLine("==BIG==" + f);
            return f;
        }

        ListBox ShowList(ListBox listBox, object[] list)
        {
            listBox.Items.Clear();
            listBox.Items.AddRange(list);

            listBox.Refresh();
            listBox.SelectedIndex = listBox.Items.Count - 1;

            return listBox;
        }

        public List<string> CheckList()
        {
            var OKlist = new List<int>();
            var fList = new List<int>();

            foreach (var p in Parts.ToArray().Reverse())
            {
                var f = p.Key;
                var n = p.Value;
                var fp = FullPath(f);

                if (File.Exists(fp) && !OKlist.Contains(n))
                {
                    OKlist.Add(n);
                    if (history[n] == null)
                    {
                        history[n] = Convert.ToBase64String(File.ReadAllBytes(fp));
                    }
                }
                else
                {
                    Parts.Remove(f);
                }
            }

            foreach (var p in FailedList.ToArray())
            {
                var f = p.Key;
                var n = p.Value;
                var fp = FullPath(f);

                if (OKlist.Contains(n))
                {
                    if (File.Exists(fp))
                    {
                        File.Delete(fp);
                    }
                    FailedList.Remove(f);
                }
                fList.Add(n);
            }

            for (var i = 0; i < history.Length; i++)
            {
                var s = history[i];
                if (s == null && !OKlist.Contains(i) && !fList.Contains(i))
                {
                    FailedList[Path.ChangeExtension(GetBaseFileName(i), "none")] = i;
                    fList.Add(i);
                }
            }

            if (FailedList.Count > 0)
            {
                return FailedList.Keys.ToList();
            }
            return Parts.Keys.ToList();
        }

        public bool CheckDup()
        {
            var loop = true;

            while (loop)
            {
                loop = false;
                for (var i = history.Length - 1; i >= 0; i--)
                {
                    var s = history[i];
                    var j = Array.IndexOf(history, s);
                    if (s != null && j != i)
                    {
                        history[i] = null;
                        history[j] = null;
                        loop = true;
                    }
                }
            }

            foreach (var f in Parts.Keys.ToArray())
            {
                var n = Parts[f];
                var fn = FullPath(f);

                if (history[n] == null)
                {
                    if (File.Exists(fn))
                    {
                        if (BaseFile.Contains(f))
                        {
                            history[n] = Convert.ToBase64String(File.ReadAllBytes(fn));
                            continue;
                        }
                        else
                        {
                            File.Delete(fn);
                        }
                    }
                    Parts.Remove(f);
                    loop = true;
                }
            }

            return loop;
        }

        public bool Next(Mainform mainform, CaptureForm cform, bool Next = true) => ThreadPool.QueueUserWorkItem(s =>
                {
                    var list = new List<string>();
                    var status = "";
                    if (repair && Next)
                    {
                        list.Add($"ReScan {PartNo} OK!");
                        foreach (var k in FailedList.Keys.ToArray())
                        {
                            if (PartNo - 1 == FailedList[k])
                            {
                                FailedList.Remove(k);
                            }
                        }
                    }
                    else if (repair)
                    {
                        list.Add($"ReScan {PartNo} Failed!");
                    }

                    if (_Countdown < 0 || Countdown < PartNo)
                    {
                        Thread.Sleep(1000);
                        status = "Finish! Failed Above!";

                        try
                        {
                            //Main.Layer.LayerManager.Instance.SuspendRefresh();
                            //Main.Layer.LayerManager.Instance.ResumeRefresh();
                            mainform.Invoke(new _StartCapture(cform.Hide));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("==BIG==" + e.Message);
                        }

                        if (PartNo <= 0 && !Next)
                        {
                            list.Add("None & Stop!");
                            list.Add("Click Again!");
                        }
                        else if (FailedList.Count > 0)
                        {
                            CheckList();
                            list = FailedList.Keys.ToList();
                            list.Sort();
                            list.AddRange(list);
                            list.Add(status);
                        }
                        else if (Parts.Count <= Countdown + 1)
                        {
                            CheckList();
                            list = Parts.Keys.ToList();
                            list.Sort();
                            list.AddRange(list);
                            list.Add("OK!");
                        }
                        else
                        {
                            list = Parts.Keys.ToList();
                            list.Sort();
                            list.AddRange(list);
                            list.Add("===None===");

                            list = FailedList.Keys.ToList();
                            list.Sort();
                            list.AddRange(list);
                        }

                        list.Add(state);
                        state = "===BIG===";
                        if (Parts.Count == history.Length)
                        {
                            list.Add("!!!===Completed===!!!");
                        }
                        list.Add($"Count: {Parts.Count}/{FailedList.Count} Missed: {FirstMiss}");
                        mainform.Invoke(new _Worklist(ShowList), new object[] { mainform.WorkList, list.ToArray() });
                        return;
                    }

                    try
                    {
                        mainform.Invoke(new _StartCapture(cform.Hide));

                        //Main.Layer.LayerManager.Instance.SuspendRefresh();
                        //Main.Layer.LayerManager.Instance.ResumeRefresh();

                        //mainform.Invoke(new _StartCapture(cform.Close));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("==BIG==" + e.Message);
                    }

                    while (mainform.IsCapture)
                    {
                        Thread.Sleep(100);
                        Console.WriteLine("waiting");
                    }
                    try
                    {
                        if (Next)
                        {
                            LeftMouseClick();
                            status = "Progress: " + PartNo + "/" + Countdown;
                        }
                        else
                        {
                            status = "Retry: " + PartNo + "/" + Countdown + "/" + (MaxRetry - retry);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("==BIG==" + e.Message);
                    }

                    list = Parts.Keys.ToList();
                    //list.Sort();
                    list.AddRange(list);
                    list.Add(status);
                    list.Add(state);
                    state = "===BIG===";
                    mainform.Invoke(new _Worklist(ShowList), new object[] { mainform.WorkList, list.ToArray() });
                    mainform.Invoke(new _StartCapture(mainform.StartCapture));
                });

        public void ReScanQR(Mainform mainform, ListBox listBox)
        {
            repair = true;
            reload = false;
            var sel = listBox.SelectedItem.ToString();
            if (!FailedList.ContainsKey(sel))
            {
                listBox.Items.Remove(sel);
                Parts.Remove(sel);
                return;
            }

            var num = FailedList[sel];
            PartNo = num;
            _Countdown = -1;
            CaptureForm._ptStart = CaptureForm._ptStub;
            CaptureForm._ptEnd = CaptureForm._ptStub;
            mainform.StartCapture();
        }

        public void Assemble(Mainform mainform, ListBox listBox)
        {
            repair = true;

            var list = Parts.Keys.ToList();

            list.Sort((x, y) => Parts[x] - Parts[y]);

            var f = FullPath(Path.ChangeExtension(Filename, "bin"));
            if (File.Exists(f))
            {
                File.Delete(f);
            }
            using (var fs = new FileStream(f, FileMode.Create, FileAccess.Write))
            {
                foreach (var s in list)
                {
                    var bs = File.ReadAllBytes(FullPath(s));
                    fs.Write(bs, 0, bs.Length);
                    listBox.Items.Insert(0, "Read: " + s);
                }
            }

            listBox.Items.Insert(0, "SaveTo: " + f);
            listBox.Refresh();
        }

        //[DllImport("user32.dll")]
        //public static extern int SetForegroundWindow(IntPtr hWnd);

        //static void Presskey()
        //{
        //    var processes = Process.GetProcesses();

        //    foreach (var proc in processes)
        //    {
        //        Console.WriteLine("==BIG==proc: " + proc.ProcessName);
        //        //SetForegroundWindow(proc.MainWindowHandle);
        //        SendKeys.SendWait("{RIGHT}");
        //    }
        //}

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TagPOINT
        {
            public int x;
            public int y;
        };
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetCursorPos(out TagPOINT lpPoint);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        //This simulates a left mouse click
        public static void LeftMouseClick()
        {
            TagPOINT cur;
            GetCursorPos(out cur);
            mouse_event(MOUSEEVENTF_LEFTDOWN, cur.x, cur.y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, cur.x, cur.y, 0, 0);
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; //最左坐标
            public int Top; //最上坐标
            public int Right; //最右坐标
            public int Bottom; //最下坐标
        }

        public static Point GetInputBoxPos(IntPtr hWnd)
        {
            var fx = new RECT();
            GetWindowRect(hWnd, ref fx);
            var width = fx.Right - fx.Left;
            var height = fx.Bottom - fx.Top;
            var x = fx.Left;
            var y = fx.Top;

            Barcode.TagPOINT cur;
            Barcode.GetCursorPos(out cur);

            var inputboxX = 450;
            var inputboxY = 150;

            if (cur.x > fx.Right || cur.y > fx.Bottom)
            {
                return new Point(cur.x, cur.y);
            }

            var t = Math.Min(cur.y, fx.Top - inputboxY);
            var l = Math.Min(cur.x, fx.Left - inputboxX);
            return new Point(l, t);

        }
    }
}
