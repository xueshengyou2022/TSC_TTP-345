using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class PrintHelpter
    {
        /// <summary>
        /// 给打印机发送打印数据
        /// </summary>
        /// <param name="json">要打印的数据</param>
        /// <returns>返回1=表示打印成功，0=失败</returns>
        public static int PrintMsg(string pihao)
        {
            //连接打印机
            TSCLIBDLL.openport("TSC TTP-345");
            //读取打印机状态，0=待机中，2=卡纸，4 =缺纸
            int state = TSCLIBDLL.usbportqueryprinter();
            Console.WriteLine("打印机状态=" + state);
            if (state != 0)
            {
                Console.WriteLine("打印机状态异常，不能打印");
                return 0;
            }

            pihao = "AC20260429001";
            string str = "20260429-1000-0001-050-00096-001";
            string sccs = "山东时真生物科技有限公司";
            string gg = "1000";
            string scrq = DateTime.Now.ToString("D");
            string yxqz = DateTime.Now.AddYears(3).ToString("D");
            // 等间距信息行（ARIAL）
            int leftX = 80;
            int startY = 240;
            int endY = 550;
            int lineCount = 5;//5行
            int fontHeight = 50;
            int totalTextHeight = fontHeight * lineCount;
            int availableHeight = endY - startY;
            int padding = (availableHeight - totalTextHeight) / (lineCount + 1);
            int y0 = startY + padding;
            int y1 = y0 + fontHeight + padding;
            int y2 = y1 + fontHeight + padding;
            int y3 = y2 + fontHeight + padding;
            int y4 = y3 + fontHeight + padding;

            int num = 2;
            //统一设置纸张格式
            TSCLIBDLL.sendcommand(@"SIZE 80 mm,60 mm
                                    GAP 2.5 mm,0 mm
                                    DIRECTION 1");
            //循环打印
            for (int i = 1; i <= num; i++)
            {
                TSCLIBDLL.nobackfeed();//设置不回吃纸
                TSCLIBDLL.clearbuffer();//只清图形，不清纸张位置
                int yOffset = (i == 1) ? 0 : 0; // 60mm * 12 dots/mm (DPI=300)
                //文本
                TSCLIBDLL.windowsfont(300, 90 + yOffset, 60, 0, 0, 0, "SimHei", $"一次性吸头");//mm*12=dot
                TSCLIBDLL.windowsfont(leftX, y0 + yOffset, fontHeight, 0, 0, 0, "ARIAL", $"规格：{gg}uL，96支/盒");
                TSCLIBDLL.windowsfont(leftX, y1 + yOffset, fontHeight, 0, 0, 0, "ARIAL", $"批号：{pihao}");
                TSCLIBDLL.windowsfont(leftX, y2 + yOffset, fontHeight, 0, 0, 0, "ARIAL", $"生产日期：{scrq}");
                TSCLIBDLL.windowsfont(leftX, y3 + yOffset, fontHeight, 0, 0, 0, "ARIAL", $"有效期至：{yxqz}");
                TSCLIBDLL.windowsfont(leftX, y4 + yOffset, fontHeight, 0, 0, 0, "ARIAL", $"生产厂商：{sccs}");
                //二维码
                TSCLIBDLL.sendcommand(@$"QRCODE 690,{240 + yOffset},M,7,A,0,M2,""20260429-1000-{i.ToString("D4")}-050-00096-001""");
                //公司LOGO
                TSCLIBDLL.sendcommand(@$"PUTBMP 80,{50 + yOffset},""LOGO.BMP""");
                TSCLIBDLL.printlabel("1", "1");
            }
            TSCLIBDLL.sendcommand("HOME");//打印完成纸张额外出纸，方便撕
            TSCLIBDLL.closeport();
            return 1;
        }

        static string GetErrorMessage(int code)
        {
            return code switch
            {
                0 => "成功",
                -1 => "端口未打开",
                -2 => "文件名无效（长度超限/非法字符）",
                -3 => "内存不足",
                -4 => "BMP 格式错误（非 BI_RGB / 位深不支持）",
                -5 => "文件数据损坏",
                _ => $"未知错误码 {code}"
            };
        }
    }
}
