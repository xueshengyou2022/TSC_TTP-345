using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class TSCLIBDLL
    {
        [DllImport("TSCLIB.dll", EntryPoint = "about")]
        public static extern int about();

        [DllImport("TSCLIB.dll", EntryPoint = "openport")]
        public static extern int openport(string printername);

        [DllImport("TSCLIB.dll", EntryPoint = "barcode")]
        public static extern int barcode(string x, string y, string type,
                    string height, string readable, string rotation,
                    string narrow, string wide, string code);

        [DllImport("TSCLIB.dll", EntryPoint = "clearbuffer")]
        public static extern int clearbuffer();

        [DllImport("TSCLIB.dll", EntryPoint = "closeport")]
        public static extern int closeport();

        [DllImport("TSCLIB.dll", EntryPoint = "downloadpcx")]
        public static extern int downloadpcx(string filename, string image_name);

        // ✅ 关键：使用 downloadbmp（不是 download！）
        [DllImport("TSCLIB.dll", EntryPoint = "downloadbmp")]
        public static extern int downloadbmp(string fileName, int fileSize, byte[] fileData);

        // ✅ 放置 BMP 图片
        [DllImport("TSCLIB.dll", EntryPoint = "putbmp")]
        public static extern int putbmp(int x, int y, string fileName);


        [DllImport("TSCLIB.dll", EntryPoint = "formfeed")]
        public static extern int formfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "nobackfeed")]
        public static extern int nobackfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "printerfont")]
        public static extern int printerfont(string x, string y, string fonttype,
                        string rotation, string xmul, string ymul,
                        string text);

        [DllImport("TSCLIB.dll", EntryPoint = "printlabel")]
        public static extern int printlabel(string set, string copy);

        //功能：繪製QRCODE二維條碼
        //語法：
        //QRCODE X, Y, ECC Level, cell width, mode, rotation, [model, mask,]"Data string”
        //參數說明
        //X QRCODE條碼左上角X座標
        //Y QRCODE條碼左上角Y座標
        //ECC level 錯誤糾正能力等級
        //L 7%
        //M 15%
        //Q 25%
        //H 30%
        //cell width    1~10
        //mode  自動生成編碼/手動生成編碼
        //A Auto
        //M Manual
        //rotation  順時針旋轉角度
        //0 不旋轉
        //90    順時針旋轉90度
        //180   順時針旋轉180度
        //270   順時針旋轉270度
        //model 條碼生成樣式
        //1 (預設), 原始版本
        //2 擴大版本
        //mask  範圍：0~8，預設7
        //Data string   條碼資料內容
        //string printercommand = "QRCODE 176,8,Q,8,A,0,M2,S7,\"" + barCode + "\"";
        [DllImport("TSCLIB.dll", EntryPoint = "sendcommand")]
        public static extern int sendcommand(string printercommand);

        [DllImport("TSCLIB.dll", EntryPoint = "sendBinaryData")]
        public static extern int sendBinaryData(byte[] str, int length);

        /// <summary>
        ///   宽度、高度、速度、浓度
        //    sensor为0：vertical 垂直间距距离 offset垂直间距的偏移
        //    sensor为1：vertical定义黑标高度和额外送出长度 offset黑标偏移量
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="speed">列印速度，1~6，6为最快速度</param>
        /// <param name="density">打印浓度，1-15，数字越大越黑</param>
        /// <param name="sensor"></param>
        /// <param name="vertical"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "setup")]
        public static extern int setup(string width, string height, string speed, string density, string sensor, string vertical, string offset);

        /// <summary>
        /// windowsfont(a,b,c,d,e,f,g,h) 
        ///       说明:使用Windows TTF字型列印文字
        ///   参数: 
        ///   a:整数型别，文字X方向起始点，以点(point)表示。 
        ///   b:整数型别，文字Y方向起始点，以点(point)表示。 
        ///   c:整数型别，字体高度，以点(point)表示。 
        ///   d:整数型别，旋转角度，逆时钟方向旋转 
        ///   0 -> 0 degree 
        ///   90-> 90 degree 
        ///   180-> 180 degree 
        ///   270-> 270 degree
        ///   e:整数型别，字体外形 
        ///   0->标准(Normal)
        ///   1->斜体(Italic)
        ///   2->粗体(Bold)
        ///   3->粗斜体(Bold and Italic)
        ///   f:整数型别,底线 
        ///   0->无底线 
        ///   1->加底线
        ///   g:字串型别，字体名称。如: Arial, Times new Roman,细名体,标楷体
        ///   h:字串型别，列印文字内容。 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="fontheight"></param>
        /// <param name="rotation"></param>
        /// <param name="fontstyle"></param>
        /// <param name="fontunderline"></param>
        /// <param name="szFaceName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "windowsfont")]
        public static extern int windowsfont(int x, int y, int fontheight,
                        int rotation, int fontstyle, int fontunderline,
                        string szFaceName, string content);

        /// <summary>
        /// 获取打印机状态，
        /// </summary>
        /// <returns></returns>
        /// 0=待机中
        /// 1=印字头开启
        /// 2=卡纸
        /// 3
        /// 4 =缺纸
        /// 10= 暂停中
        /// 20 =列印中
        [DllImport("TSCLIB.dll", EntryPoint = "usbportqueryprinter")]
        public static extern int usbportqueryprinter();
    }
}
