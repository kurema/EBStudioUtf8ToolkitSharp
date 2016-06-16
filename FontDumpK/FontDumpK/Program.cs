using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FontDumpK
{
    class Program
    {
        static void Main(string[] args)
        {
            var argList = new List<string>();
            var argOption = new Dictionary<string, string>();
            foreach (var arg in args)
            {
                if (arg.Contains("="))
                {
                    var list = arg.Split(new char[] { '=' }, 2);
                    argOption[list[0].ToLower()] = list[1];
                }
                else { argList.Add(arg); }
            }

            var fontfamily = argList[0];
            var textPath = argList[1];
            var textOutPath = argList[2];

            string origin;
            using(var sr=new System.IO.StreamReader(textPath))
            {
                origin = sr.ReadToEnd();
            }

            if (argOption.ContainsKey("-descent")&& argOption["-descent"] == "false")
            {
                IncludeDescent = false;
            }
            string mapPath = "";
            if (argOption.ContainsKey("-map"))
            {
                mapPath = argOption["-map"];
            }

            var result = ConvertGaiji(origin, new FontFamily(fontfamily), "Gaiji.xml", "GaijiMap.xml", mapPath,16, 24, 48);
            using (var sw = new System.IO.FileStream(textOutPath,System.IO.FileMode.Create,System.IO.FileAccess.Write))
            {
                sw.Write(result, 0, result.Length);
            }
        }

        public static Dictionary<string,string> DefaultAltDictionary
        {
            get
            {
                return new Dictionary<string, string>() { { "BE", "3/4" }, { "C5", "A" }, { "16F", "u" }, { "137", "k" }, { "17C", "z" }, { "15C", "S" }, { "12B", "i" }, { "E1", "a" }, { "140", "l" }, { "EE", "i" }, { "DB", "U" }, { "159", "r" }, { "165", "t" }, { "138", "k" }, { "129", "i" }, { "FC", "u" }, { "16C", "U" }, { "12E", "I" }, { "14F", "o" }, { "BA", "0" }, { "112", "E" }, { "B1", "+-" }, { "D3", "O" }, { "C4", "A" }, { "144", "n" }, { "174", "W" }, { "14D", "o" }, { "145", "N" }, { "D9", "U" }, { "F0", "d" }, { "162", "T" }, { "F9", "u" }, { "E0", "a" }, { "DC", "U" }, { "D0", "D" }, { "10C", "C" }, { "157", "r" }, { "17B", "Z" }, { "173", "u" }, { "EC", "i" }, { "16D", "u" }, { "12D", "i" }, { "163", "t" }, { "E9", "e" }, { "135", "j" }, { "125", "h" }, { "139", "L" }, { "151", "o" }, { "14B", "n" }, { "C8", "E" }, { "B3", "3" }, { "12F", "i" }, { "D1", "N" }, { "DE", "P" }, { "100", "A" }, { "161", "s" }, { "192", "f" }, { "CC", "I" }, { "F8", "o" }, { "CF", "I" }, { "168", "U" }, { "C6", "AE" }, { "11A", "E" }, { "116", "E" }, { "D4", "O" }, { "155", "r" }, { "176", "Y" }, { "FE", "p" }, { "11F", "g" }, { "156", "R" }, { "15E", "S" }, { "128", "I" }, { "172", "U" }, { "E7", "c" }, { "DA", "U" }, { "11C", "G" }, { "170", "U" }, { "FB", "u" }, { "147", "N" }, { "134", "J" }, { "14E", "O" }, { "158", "R" }, { "EB", "e" }, { "15D", "s" }, { "E6", "ae" }, { "14A", "N" }, { "11E", "G" }, { "167", "t" }, { "13C", "l" }, { "121", "g" }, { "153", "oe" }, { "C9", "E" }, { "F6", "o" }, { "104", "A" }, { "CD", "I" }, { "12C", "I" }, { "F7", "/" }, { "E8", "e" }, { "177", "y" }, { "A1", "i" }, { "10E", "D" }, { "10D", "c" }, { "111", "d" }, { "169", "u" }, { "124", "H" }, { "119", "e" }, { "10B", "c" }, { "123", "g" }, { "16B", "u" }, { "D6", "O" }, { "133", "ij" }, { "17D", "Z" }, { "F1", "n" }, { "117", "e" }, { "15A", "S" }, { "A9", "(C)" }, { "115", "e" }, { "150", "O" }, { "C7", "C" }, { "179", "Z" }, { "175", "w" }, { "C2", "A" }, { "13F", "L" }, { "E2", "a" }, { "BD", "1/2" }, { "10F", "d" }, { "146", "n" }, { "DD", "Y" }, { "C1", "A" }, { "BC", "1/4" }, { "16A", "U" }, { "17E", "z" }, { "ED", "i" }, { "13B", "L" }, { "141", "L" }, { "160", "S" }, { "118", "E" }, { "E5", "a" }, { "166", "T" }, { "11B", "e" }, { "15F", "s" }, { "D8", "O" }, { "C0", "A" }, { "164", "T" }, { "13E", "l" }, { "142", "l" }, { "106", "C" }, { "D7", "X" }, { "171", "u" }, { "108", "C" }, { "149", "n" }, { "122", "G" }, { "17A", "z" }, { "178", "Y" }, { "152", "OE" }, { "F5", "o" }, { "C3", "A" }, { "B9", "1" }, { "FA", "u" }, { "148", "n" }, { "EF", "i" }, { "102", "A" }, { "110", "D" }, { "105", "a" }, { "CB", "E" }, { "10A", "C" }, { "B2", "2" }, { "114", "E" }, { "14C", "O" }, { "154", "R" }, { "101", "a" }, { "16E", "U" }, { "F3", "o" }, { "13A", "l" }, { "FD", "y" }, { "136", "K" }, { "AE", "(R)" }, { "CE", "I" }, { "143", "N" }, { "120", "G" }, { "113", "e" }, { "DF", "B" }, { "EA", "e" }, { "11D", "g" }, { "15B", "s" }, { "F4", "o" }, { "103", "a" }, { "E3", "a" }, { "CA", "E" }, { "109", "c" }, { "132", "IJ" }, { "D2", "O" }, { "13D", "L" }, { "E4", "a" }, { "FF", "y" }, { "130", "I" }, { "D5", "O" }, { "F2", "o" }, { "131", "i" }, { "126", "H" }, { "127", "h" }, { "12A", "I" }, { "107", "c" }, };
            }
        }

        public static byte[] ConvertGaiji(string text,FontFamily ff,string gaijiDataPath,string gaijiSetPath,string ebMap, params int[] heights)
        {
            var lists = new List<string>();
            var sjis = Encode(text, ff, out lists, heights);
            var dic = new Dictionary<string, string>();
            var defaultDic = DefaultAltDictionary;
            foreach (var failed in lists)
            {
                var str = char.ConvertToUtf32(failed, 0).ToString("X");
                if (defaultDic.ContainsKey(str))
                {
                    dic[str] = defaultDic[str];
                }
                else { dic[str] = null; }
            }
            gaijiData gd;
            gaijiSet gs;
            string map;
            CreateSetFromString(dic, ff, out gd, out gs,out map, heights);
            {
                var ser = new System.Xml.Serialization.XmlSerializer(typeof(gaijiData));
                using (var sw = new System.IO.StreamWriter(gaijiDataPath))
                {
                    ser.Serialize(sw, gd);
                }
            }
            {
                var ser = new System.Xml.Serialization.XmlSerializer(typeof(gaijiSet));
                using (var sw = new System.IO.StreamWriter(gaijiSetPath))
                {
                    ser.Serialize(sw, gs);
                }
            }

            if (ebMap != "")
            {
                using (var ebsw = new System.IO.StreamWriter(ebMap, false, Encoding.GetEncoding("shift_jis")))
                {
                    ebsw.Write(map);
                }
            }

            return sjis;
        }

        public static byte[] Encode(string text, FontFamily ff, out List<string> failed, params int[] heights)
        {
            failed = new List<string>();
            var sjis = Encoding.GetEncoding("Shift_JIS", new EncoderEscapeFallback() { failedLists = failed }, DecoderFallback.ReplacementFallback);
            return (sjis.GetBytes(text));
        }

        public class EncoderEscapeFallback : EncoderFallback
        {
            public List<string> failedLists;

            public override int MaxCharCount
            {
                get
                {
                    return 10;
                }
            }

            public override EncoderFallbackBuffer CreateFallbackBuffer()
            {
                return new EncoderEscapeFallbackBuffer() { failedLists = this.failedLists };
            }
        }

        public class EncoderEscapeFallbackBuffer : EncoderFallbackBuffer
        {
            private char[] alternative;
            private int offset;
            public List<string> failedLists;

            public override int Remaining
            {
                get
                {
                    return alternative.Length - offset;
                }
            }

            public override bool Fallback(char charUnknown, int index)
            {
                alternative = string.Format("&#x{0:X};", (int)charUnknown).ToCharArray();
                offset = 0;

                failedLists.Add(charUnknown.ToString());

                return true;
            }

            public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
            {
                var letter = charUnknownHigh.ToString() + charUnknownLow.ToString();
                alternative = string.Format("&#" + char.ConvertToUtf32(letter,0).ToString("X") + ";", (int)charUnknownHigh, (int)charUnknownLow).ToCharArray();
                offset = 0;

                failedLists.Add(letter);

                return true;
            }

            public override char GetNextChar()
            {
                if (alternative.Length <= offset)
                {
                    return char.MinValue;
                }
                else
                {
                    return alternative[offset++];
                }
            }

            public override bool MovePrevious()
            {
                throw new NotImplementedException();
            }
        }

        public static void CreateSetFromString(Dictionary<string, string> target, FontFamily ff, out gaijiData gd, out gaijiSet gs,out string ebMap, params int[] heights)
        {
            int ebcInitial = 0xA121;

            var fontDatasFull = new List<fontData>[heights.Count()];
            var fontDatasHalf = new List<fontData>[heights.Count()];
            var fontSizes = new float[heights.Count()];
            for (int i = 0; i < heights.Count(); i++)
            {
                fontDatasFull[i] = new List<fontData>();
                fontDatasHalf[i] = new List<fontData>();
                fontSizes[i] = AssumeFontSize(ff, heights[i]);
            }

            var gmfull = new List<gaijiSetGaijiMap>();
            var gmhalf = new List<gaijiSetGaijiMap>();

            foreach (var kvp in target)
            {
                if (kvp.Key == " " || kvp.Key == "\r" || kvp.Key == "\n")
                {
                    continue;
                }
                gaijiSetGaijiMap map = new gaijiSetGaijiMap();
                map.unicode = "#x"+kvp.Key;
                map.alt = kvp.Value;
                map.ebcode = ebcInitial.ToString("X");

                bool full = false;
                for (int i = 0; i < heights.Count(); i++)
                {
                    bool dummy;
                    fontData result;
                    if (i != 0)
                    {
                        result = (GetFontData(map, ff, heights[i], i == 0 ? 0 : (full ? AutoDetectFull(heights[i]) : AutoDetectHalf(heights[i])), fontSizes[i], out dummy));
                    }
                    else
                    {
                        result = (GetFontData(map, ff, heights[i], 0, fontSizes[i], out full));
                    }
                    if (full)
                    {
                        fontDatasFull[i].Add(result);
                    }
                    else
                    {
                        fontDatasHalf[i].Add(result);
                    }
                }
                if (full)
                {
                    gmfull.Add(map);
                }
                else
                {
                    gmhalf.Add(map);
                }
            }

            var ebmapBuilder = new StringBuilder();

            int ebc = ebcInitial;
            for (int i = 0; i < gmhalf.Count(); i++)
            {
                gmhalf[i].ebcode = ebc.ToString("X");
                for (int j = 0; j < heights.Count(); j++)
                {
                    fontDatasHalf[j][i].ebcode = ebc.ToString("X");
                }
                if (gmfull[i].unicode.Length < 7)
                {
                    ebmapBuilder.AppendLine("h" + ebc.ToString("X") + "\tu" + new String('0', Math.Max(6 - gmhalf[i].unicode.Length, 0)) + gmhalf[i].unicode.Substring(2) + "\t" + gmhalf[i].alt);
                }
                ebc =IncrementEbcode(ebc);
            }
            int ebcFullStart = ebc;
            for (int i = 0; i < gmfull.Count(); i++)
            {
                gmfull[i].ebcode = ebc.ToString("X");
                for (int j = 0; j < heights.Count(); j++)
                {
                    fontDatasFull[j][i].ebcode = ebc.ToString("X");
                }
                if (gmfull[i].unicode.Length < 7)
                {
                    ebmapBuilder.AppendLine("z" + ebc.ToString("X") + "\tu" + new String('0', Math.Max(6 - gmfull[i].unicode.Length, 0)) + gmfull[i].unicode.Substring(2) + "\t" + gmfull[i].alt);
                }
                ebc = IncrementEbcode(ebc);
            }
            ebMap = ebmapBuilder.ToString();

            gd = new gaijiData();
            gd.fontSet = new gaijiDataFontSet[heights.Count() * 2];
            for (int i = 0; i < heights.Count(); i++)
            {
                var gds = new gaijiDataFontSet();
                gds.start = ebcInitial.ToString("X");
                gds.fontData = fontDatasHalf[i].ToArray();
                gds.size = AutoDetectHalf(heights[i]) + "X" + heights[i];
                gd.fontSet[i] = gds;
            }
            for (int i = 0; i < heights.Count(); i++)
            {
                var gds = new gaijiDataFontSet();
                gds.start = ebcFullStart.ToString("X");
                gds.fontData = fontDatasFull[i].ToArray();
                gds.size = AutoDetectFull(heights[i]) + "X" + heights[i];
                gd.fontSet[i + heights.Count()] = gds;
            }
            gmhalf.AddRange(gmfull);
            gs = new gaijiSet() { gaijiMap = gmhalf.ToArray() };
        }

        public static gaijiData CreateGaijiDataFromGaijiSet(gaijiSet set, FontFamily fontFamily, int height, int width)
        {
            if (width == 0)
            {
                throw new Exception("Auto detection is invalid for gaijiSet conversion.");
            }
            var fontDatas = new List<fontData>();

            float fontsize = AssumeFontSize(fontFamily, height);

            foreach (var map in set.gaijiMap)
            {
                bool full;
                fontDatas.Add(GetFontData(map, fontFamily, height, width, fontsize, out full));
            }
            var result = new gaijiData();
            result.fontSet = new gaijiDataFontSet[] { new gaijiDataFontSet() { size = width + "X" + height, start = set.gaijiMap[0].ebcode, fontData = fontDatas.ToArray() } };
            return result;
        }

        public static bool IncludeDescent = true;

        public static float AssumeFontSize(FontFamily fontFamily,int height)
        {
            Bitmap canvas = new Bitmap(AutoDetectFull(height), height);
            var g = Graphics.FromImage(canvas);
            var font = new Font(fontFamily, height);
            StringFormat sf = new StringFormat(StringFormat.GenericTypographic);
            var size = g.MeasureString("aAあ漢", font,height*100);

            var AscentAndDescent = (fontFamily.GetCellAscent(FontStyle.Regular) + (IncludeDescent ? fontFamily.GetCellDescent(FontStyle.Regular) : 0));
            var Spacing = fontFamily.GetLineSpacing(FontStyle.Regular);

            return (float)height * height / size.Height * (Spacing) / AscentAndDescent;
            //return (float)height * height / size.Height;
        }

        public static fontData GetFontData(gaijiSetGaijiMap map, FontFamily fontFamily, int height,int width,float fontsize,out bool full)
        {
            var bmp = GetFontBitmap(fontFamily, GetUnicodeCharacter(map.unicode.Substring(2)), height, width, fontsize);
            var content = GetImageString(bmp);
            var gd = new fontData();
            gd.ebcode = map.ebcode;
            gd.unicode = map.unicode.Substring(2);
            gd.Value = content;
            full = bmp.Width == AutoDetectFull(height);
            return gd;
        }

        public static string GetImageString(Bitmap bmp,char emptyLetter=' ')
        {
            string result = "\r\n";
            for(int i = 0; i < bmp.Height; i++)
            {
                for(int j = 0; j < bmp.Width; j++)
                {
                    var col = bmp.GetPixel(j, i);
                    if (col.R < 127)
                    {
                        result += "#";
                    }else
                    {
                        result += emptyLetter;
                    }
                }
                result += "\r\n";
            }
            return result;
        }

        public static int IncrementEbcode(int ebc)
        {
            ebc++;
            if ((ebc - 0xA121) % 256 > 93)
            {
                ebc += 256 - 94;
            }
            return ebc;
        }

        public static string GetUnicodeCharacter(string hex)
        {
            return char.ConvertFromUtf32(Convert.ToInt32(hex, 16)); ;
        }

        public static Bitmap GetFontBitmap(FontFamily fontFamily,string unicode,int height,int width,float fontsize=0)
        {
            string text = unicode;
            bool AutoDetectWidth = width <= 0;
            if (AutoDetectWidth)
            {
                width = AutoDetectFull(height);
            }

            Bitmap canvas = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(canvas);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            var font = new Font(fontFamily, fontsize != 0 ? fontsize : height);
            StringFormat sf = new StringFormat(StringFormat.GenericTypographic);
            var size = g.MeasureString(text, font, height * 100, sf);
            if (fontsize == 0)
            {
                font = new Font(fontFamily, (float)height * height / size.Height);
                size = g.MeasureString(text, font, width, sf);
            }
            if (AutoDetectWidth)
            {
                if (size.Width <= AutoDetectHalf(height))
                {
                    width = AutoDetectHalf(height);
                    canvas = new Bitmap(width, height);
                    g = Graphics.FromImage(canvas);
                }
            }
            var AscentAndDescent = (fontFamily.GetCellAscent(FontStyle.Regular) + (IncludeDescent ? fontFamily.GetCellDescent(FontStyle.Regular) : 0));
            var Spacing = fontFamily.GetLineSpacing(FontStyle.Regular);

            g.Clear(Color.White);
            g.DrawString(text, font, Brushes.Black, (width - size.Width) / 2.0f, (height - size.Height * AscentAndDescent / Spacing) / 2.0f, sf);
            return canvas;
        }

        public static int AutoDetectFull(int height)
        {
            return height == 30 ? 32 : height;
        }

        public static int AutoDetectHalf(int height)
        {
            return height == 30 ? 16 : height / 2;
        }
    }
}
