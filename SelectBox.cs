using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SelectBoxDll
{
    public enum AlignmentEnum { Left, Center, Right }
    public enum PreBorderEnum
    {
        [Description("全细边框")] AllThinLine,
        [Description("全粗边框")] AllBoldLine,
        [Description("全双线边框")] AllDoubleLine,
        [Description("圆角细边框")] ThinBorderRadius,
        [Description("仅粗竖线")] BoldVerticalLineOnly,
        [Description("仅粗横线")] BoldHorizontalLineOnly,
        [Description("仅竖双线")] DoubleVerticalLineOnly,
        [Description("仅横双线")] DoubleHorizontalLineOnly,
    }

    #region 使用到的构造体变量集
    public struct Position
    {
        public Int32 Left, Top;
        public Position(Boolean init) { Left = Console.CursorLeft; Top = Console.CursorTop; }
        public Position(Int32 x, Int32 y) { Left = x; Top = y; }
    }
    public struct Colors
    {
        public ConsoleColor ForegroundColor, BackgroundColor;
        public Colors(Boolean init) { ForegroundColor = Console.ForegroundColor; BackgroundColor = Console.BackgroundColor; }
        public Colors(ConsoleColor fg, ConsoleColor bg) { ForegroundColor = fg; BackgroundColor = bg; }
    }
    public struct MarginPaddingObj
    {
        public Int32 Buttom, Top, Left, Right;
        public Int32 Width, Height;
        public Position Position;
        public MarginPaddingObj(Boolean init) { Buttom = Top = Left = Right = -1; Width = Height = 0; Position = new Position(true); }
        public MarginPaddingObj(Int32 left, Int32 top)
        { Left = left; Top = top; Buttom = Right = -1; Width = Height = 0; Position = new Position(true); }
        public MarginPaddingObj(Int32 left, Int32 top, Int32 right, Int32 buttom)
        { Left = left; Top = top; Right = right; Buttom = buttom; Width = Height = 0; Position = new Position(true); }
    }
    public struct BorderObj
    {
        public String Buttom, Top, Left, Right;
        public String LeftButtom, RightButtom;
        public String LeftTop, RightTop;
        public Int32 Width, Height;
        public Int32 LeftMaxLen, RightMaxLen, TopMaxLen, ButtomMaxLen;
        public Position Position;
        public BorderObj(Boolean init)
        {
            Buttom = Top = Left = Right = null;
            LeftButtom = RightButtom = null;
            LeftTop = RightTop = null;
            LeftMaxLen = RightMaxLen = TopMaxLen = ButtomMaxLen = Width = Height = 0;
            Position = new Position(true);
        }
        public BorderObj(String left, String top, String lefttop, String righttop, String leftButtom, String rightButtom)
        {
            Left = left; Top = top;
            Right = Buttom = null;
            LeftButtom = leftButtom; RightButtom = rightButtom; LeftTop = lefttop; RightTop = righttop;
            LeftMaxLen = RightMaxLen = TopMaxLen = ButtomMaxLen = Width = Height = 0;
            Position = new Position(true);
        }
        public BorderObj(String left, String top, String right, String buttom, String lefttop, String righttop, String leftButtom, String rightButtom)
        {
            Left = left; Top = top; Right = right; Buttom = buttom;
            LeftButtom = leftButtom; RightButtom = rightButtom; LeftTop = lefttop; RightTop = righttop;
            LeftMaxLen = RightMaxLen = TopMaxLen = ButtomMaxLen = Width = Height = 0;
            Position = new Position(true);
        }
        public BorderObj(PreBorderEnum custom)
        {
            Dictionary<PreBorderEnum, String[]> PreBorderDict = new Dictionary<PreBorderEnum, String[]>();
            PreBorderDict.Add(PreBorderEnum.AllThinLine, new String[] { "\u2502", "\u2500", "\u250C", "\u2510", "\u2514", "\u2518" });
            PreBorderDict.Add(PreBorderEnum.AllBoldLine, new String[] { "\u2503", "\u2501", "\u250F", "\u2513", "\u2517", "\u251B" });
            PreBorderDict.Add(PreBorderEnum.AllDoubleLine, new String[] { "\u2551", "\u2550", "\u2554", "\u2557", "\u255A", "\u255D" });
            PreBorderDict.Add(PreBorderEnum.ThinBorderRadius, new String[] { "\u2502", "\u2500", "\u256D", "\u256E", "\u2570", "\u256F" });
            PreBorderDict.Add(PreBorderEnum.BoldVerticalLineOnly, new String[] { "\u2503", "\u2500", "\u250E", "\u2512", "\u2516", "\u251A" });
            PreBorderDict.Add(PreBorderEnum.BoldHorizontalLineOnly, new String[] { "\u2502", "\u2501", "\u250D", "\u2511", "\u2515", "\u2519" });
            PreBorderDict.Add(PreBorderEnum.DoubleVerticalLineOnly, new String[] { "\u2551", "\u2501", "\u2553", "\u2556", "\u2559", "\u255C" });
            PreBorderDict.Add(PreBorderEnum.DoubleHorizontalLineOnly, new String[] { "\u2502", "\u2550", "\u2552", "\u2555", "\u2558", "\u255B" });

            String[] PreBorder = PreBorderDict[custom];
            Left = Right = PreBorder[0]; Top = Buttom = PreBorder[1];
            LeftTop = PreBorder[2]; RightTop = PreBorder[3];
            LeftButtom = PreBorder[4]; RightButtom = PreBorder[5];
            LeftMaxLen = RightMaxLen = TopMaxLen = ButtomMaxLen = Width = Height = 0;
            Position = new Position(true);
        }
    }
    public struct ContentObj
    {
        public String[] Strings, Flush;
        public String Pointer;
        public Position[] Positions;
        public ContentObj(Boolean init)
        {
            Strings = Flush = new String[1] { null };
            Pointer = null;
            Positions = new Position[1] { new Position(true) };
        }
        public ContentObj(String pointer, params String[] strs)
        {
            Strings = strs; Pointer = pointer;
            Flush = new String[1] { null };
            Positions = new Position[1] { new Position(true) };
        }
    }
    public struct ContentSetObj
    {
        public Int32 Column, Row, Count;
        public Int32 Width, Height;
        public Int32[] ColWidth, RowHeight;
        public Int32[] ColIndent, RowIndent;
        public Boolean ByColumn;
        public AlignmentEnum Alignment;
        public Position Position;
        public ContentSetObj(Boolean init)
        {
            Column = Row = Count = Width = Height = 0;
            ColWidth = RowHeight = new Int32[1] { 0 };
            ColIndent = RowIndent = new Int32[1] { -1 };
            ByColumn = false; Alignment = AlignmentEnum.Center;
            Position = new Position(true);
        }
        public ContentSetObj(Int32 col, Int32[] colindent, Boolean bycol, AlignmentEnum alignment)
        {
            Column = col; ColIndent = colindent; ByColumn = bycol; Alignment = alignment;
            Row = Count = Width = Height = 0;
            ColWidth = RowHeight = new Int32[1] { 0 };
            RowIndent = new Int32[1] { -1 };
            Position = new Position(true);
        }
    }
    public struct GraphColorsObj
    {
        public Colors Margin, Border, Padding, Content, HighLight, Pointer;
        public GraphColorsObj(Boolean init) { Margin = Border = Padding = Content = HighLight = Pointer = new Colors(true); }
    }
    public struct ReadContentObj
    {
        public String UpArrow, DownArrow, LeftArrow, RightArrow, Value;
        public Int32 Index;
        public Boolean Recycle;
        public ReadContentObj(Boolean init)
        {
            UpArrow = ConsoleKey.UpArrow.ToString();
            DownArrow = ConsoleKey.DownArrow.ToString();
            LeftArrow = ConsoleKey.LeftArrow.ToString();
            RightArrow = ConsoleKey.RightArrow.ToString();
            Index = -1; Value = null; Recycle = false;
        }
        public ReadContentObj(Boolean init, Boolean recycle)
        {
            UpArrow = ConsoleKey.UpArrow.ToString();
            DownArrow = ConsoleKey.DownArrow.ToString();
            LeftArrow = ConsoleKey.LeftArrow.ToString();
            RightArrow = ConsoleKey.RightArrow.ToString();
            Index = -1; Value = null; Recycle = recycle;
        }
        public ReadContentObj(String up, String down, String left, String right, Boolean recycle)
        {
            UpArrow = ((ConsoleKey)Enum.Parse(typeof(ConsoleKey), up)).ToString();
            DownArrow = ((ConsoleKey)Enum.Parse(typeof(ConsoleKey), up)).ToString();
            LeftArrow = ((ConsoleKey)Enum.Parse(typeof(ConsoleKey), up)).ToString();
            RightArrow = ((ConsoleKey)Enum.Parse(typeof(ConsoleKey), up)).ToString();
            Index = -1; Value = null; Recycle = recycle;
        }
    }
    #endregion

    public class Function
    {
        // 其他程序调用时的兼容方法
        public static String[] StringArray(params String[] str) { return str; }
        // TODO: 字符串的真实长度
        public static Int32 RawLength(String String) { return System.Text.Encoding.Default.GetBytes(String).Length; }
        // TODO: 字符重复多次
        public static String RepeatStr(String str, Int32 num) { return new String('+', num).Replace("+", str); }
        // TODO: 整数最大值
        public static Int32 IntMax(params Int32[] Int32s) { return Enumerable.Max(Int32s); }
        // TODO: 获取序列按指定行列排序后, 四个边界的索引
        public static Dictionary<String, Int32[]> GetBorderIndex(Int32 Count, Int32 Column, Boolean ByColumn)
        {
            Dictionary<String, Int32[]> dict = new Dictionary<String, Int32[]>();
            if (Count < Column) { Console.WriteLine("[INFO] Count is less than Column."); return dict; }
            Int32 Row = (Int32)(Math.Ceiling((Double)Count / Column));

            Int32[] Left, Right, Top, Buttom;
            if (ByColumn)
            {
                Left = Enumerable.Range(0, Row).ToArray();
                Top = Enumerable.Range(0, Column).Select(p => p * Row).ToArray();
                Buttom = Top.Select(p => { return p + Row - 1 < Count ? p + Row - 1 : Count - 1; }).ToArray();
                Right = new Int32[Row]; Enumerable.Range(Top.Last(), Count - Top.Last()).ToArray().CopyTo(Right, 0);
                if (Column * Row > Count) { Enumerable.Range(Count - Row, Row - Count + Top.Last()).ToArray().CopyTo(Right, Count - Top.Last()); }
            }
            else
            {
                Top = Enumerable.Range(0, Column).ToArray();
                Left = Enumerable.Range(0, Row).Select(p => p * Column).ToArray();
                Right = Left.Select(p => { return p + Column - 1 < Count ? p + Column - 1 : Count - 1; }).ToArray();
                Buttom = new Int32[Column]; Enumerable.Range(Left.Last(), Count - Left.Last()).ToArray().CopyTo(Buttom, 0);
                if (Column * Row > Count) { Enumerable.Range(Count - Column, Column - Count + Left.Last()).ToArray().CopyTo(Buttom, Count - Left.Last()); }
            }

            // Console.WriteLine(String.Join("-", Left.Select(p => String.Format("{0}", p)).ToArray()));
            // Console.WriteLine(String.Join("-", Right.Select(p => String.Format("{0}", p)).ToArray()));
            // Console.WriteLine(String.Join("-", Top.Select(p => String.Format("{0}", p)).ToArray()));
            // Console.WriteLine(String.Join("-", Buttom.Select(p => String.Format("{0}", p)).ToArray()));

            dict.Add("Left", Left); dict.Add("Right", Right); dict.Add("Top", Top); dict.Add("Buttom", Buttom);
            return dict;
        }
    }

    public class BoxSetting
    {
        public Position Position = new Position(true);
        public ContentObj Content = new ContentObj(true);
        public Boolean OldConsole = true;
        public ContentSetObj ContentSet = new ContentSetObj(true);
        public MarginPaddingObj Padding = new MarginPaddingObj(true);
        public BorderObj Border = new BorderObj(true);
        public MarginPaddingObj Margin = new MarginPaddingObj(true);

        // TODO: 外部定义方法
        public void NewSet(Boolean oldconsole, Position pos) { OldConsole = oldconsole; Position = pos; }
        public void NewSet(Boolean oldconsole, Position pos, MarginPaddingObj marginobj, MarginPaddingObj paddingobj, BorderObj borderobj, ContentObj contentobj, ContentSetObj contentsetobj)
        {
            NewSet(oldconsole, pos); Margin = marginobj; Padding = paddingobj;
            Border = borderobj; Content = contentobj; ContentSet = contentsetobj;
        }

        #region 内部函数
        private void MergeContent()
        {
            Content.Pointer = Content.Pointer ?? "";
            ContentSet.Count = Content.Strings.Length;
            // TODO: 默认列数为 1
            if (ContentSet.Column == 0 && ContentSet.Row == 0) { ContentSet.Column = 1; ContentSet.Row = ContentSet.Count; }
            // TODO: 依据给的行(列)数补足对应列(行)数
            else
            {
                if (ContentSet.Column > ContentSet.Count) { ContentSet.Column = ContentSet.Count; }
                if (ContentSet.Column == 0 && ContentSet.Row != 0)
                { ContentSet.Column = (Int32)(Math.Ceiling((Double)ContentSet.Count / ContentSet.Row)); }
                if (ContentSet.Column != 0 && ContentSet.Row == 0)
                {
                    ContentSet.Row = (Int32)(Math.Ceiling((Double)ContentSet.Count / ContentSet.Column));
                }
            }
            // TODO: 缩短原字符数组
            if (ContentSet.Column * ContentSet.Row < ContentSet.Count)
            {
                ContentSet.Count = ContentSet.Column * ContentSet.Row;
                Content.Strings = Content.Strings.Take(ContentSet.Count).ToArray();
            }

            // TODO: 计算列宽
            Int32 NowCol = 0, NowRow = 0, NowStrLen = 0, PointerLen = Function.RawLength(Content.Pointer);
            ContentSet.ColWidth = new Int32[ContentSet.Column];

            for (Int32 i = 0; i < ContentSet.Count; i++)
            {
                if (ContentSet.ByColumn)
                { NowRow = i % ContentSet.Row + 1; NowCol = (Int32)Math.Ceiling((Double)(i + 1) / ContentSet.Row); }
                else
                { NowCol = i % ContentSet.Column + 1; NowRow = (Int32)Math.Ceiling((Double)(i + 1) / ContentSet.Column); }
                NowStrLen = Function.RawLength(Content.Strings[i]) + PointerLen;
                ContentSet.ColWidth[NowCol - 1] = Math.Max(ContentSet.ColWidth[NowCol - 1], NowStrLen);

                // Console.WriteLine("[{0},{1}] {2} {3}", NowRow, NowCol, NowStrLen, ColWidth[NowCol - 1]);
            }

            // TODO: 生成对应列宽的空白字符串
            Content.Flush = new String[ContentSet.Column];
            for (Int32 i = 0; i < ContentSet.Column; i++) { Content.Flush[i] = new String('+', ContentSet.ColWidth[i]).Replace("+", " "); }

            // TODO: 处理列间隔数组
            if (ContentSet.ColIndent[0] == -1) { ContentSet.ColIndent = Enumerable.Repeat(1, ContentSet.Column - 1).ToArray(); }
            // 若不足
            else if (ContentSet.ColIndent.Length < ContentSet.Column - 1)
            { ContentSet.ColIndent = ContentSet.ColIndent.Concat(Enumerable.Repeat(1, ContentSet.Column - ContentSet.ColIndent.Length - 1).ToArray()).ToArray(); }
            // 若过多
            else if (ContentSet.ColIndent.Length > ContentSet.Column - 1)
            { ContentSet.ColIndent = ContentSet.ColIndent.Take(ContentSet.Column - 1).ToArray(); }
            ContentSet.ColIndent = new Int32[1].Concat(ContentSet.ColIndent).ToArray(); // 数组头部插入 0

            // TODO: 计算Content总宽度与总高度
            ContentSet.Width = ContentSet.ColWidth.Sum() + ContentSet.ColIndent.Sum();

            ContentSet.RowHeight = Enumerable.Repeat(1, ContentSet.Row).ToArray(); // HACk: 默认单行输出
            ContentSet.RowIndent = new Int32[ContentSet.Row - 1];                  // HACk: 默认零行距
            ContentSet.Height = ContentSet.RowHeight.Sum() + ContentSet.RowIndent.Sum();
        }

        private void MergePadding()
        {
            // TODO: 默认初始化
            if (Padding.Top == -1) { Padding.Top = 0; }
            if (Padding.Left == -1) { Padding.Left = 0; }
            if (Padding.Buttom == -1) { Padding.Buttom = Padding.Top; }
            if (Padding.Right == -1) { Padding.Right = Padding.Left; }

            Padding.Width = Padding.Left + Padding.Right + ContentSet.Width;
            Padding.Height = Padding.Top + Padding.Buttom + ContentSet.Height;
        }

        private void MergeBorder()
        {
            // TODO: 默认初始化
            Border.Left = Border.Left ?? "";
            // Border.Top = Border.Top ?? "";
            if (Border.Top == null || Border.Top.Length == 0) { Border.Top = " "; }
            Border.Right = Border.Right ?? Border.Left;
            Border.Buttom = Border.Buttom ?? Border.Top;
            Border.LeftTop = Border.LeftTop ?? "";
            Border.LeftButtom = Border.LeftButtom ?? "";
            Border.RightTop = Border.RightTop ?? "";
            Border.RightButtom = Border.RightButtom ?? "";

            if (OldConsole)
            {
                Border.LeftMaxLen = Function.IntMax(Function.RawLength(Border.Left), Function.RawLength(Border.LeftTop), Function.RawLength(Border.LeftButtom));
                Border.RightMaxLen = Function.IntMax(Function.RawLength(Border.Right), Function.RawLength(Border.RightTop), Function.RawLength(Border.RightButtom));
            }
            else
            {
                Border.LeftMaxLen = Function.IntMax(Border.Left.Length, Border.LeftTop.Length, Border.LeftButtom.Length);
                Border.RightMaxLen = Function.IntMax(Border.Right.Length, Border.RightTop.Length, Border.RightButtom.Length);
            }
            Border.TopMaxLen = Convert.ToInt16(Convert.ToBoolean(Border.Top.Length + Border.LeftTop.Length + Border.RightTop.Length));
            Border.ButtomMaxLen = Convert.ToInt16(Convert.ToBoolean(Border.Buttom.Length + Border.LeftButtom.Length + Border.RightButtom.Length));
            // FIXME: Top, Buttom 只能为单行或零行

            Border.Width = Border.LeftMaxLen + Border.RightMaxLen + Padding.Width;
            Border.Height = Border.TopMaxLen + Border.ButtomMaxLen + Padding.Height;
        }

        private void MergeMargin()
        {
            // TODO: 默认初始化
            if (Margin.Top == -1) { Margin.Top = 0; }
            if (Margin.Left == -1) { Margin.Left = 0; }
            if (Margin.Buttom == -1) { Margin.Buttom = Margin.Top; }
            if (Margin.Right == -1) { Margin.Right = Margin.Left; }

            Margin.Width = Margin.Left + Margin.Right + Border.Width;
            Margin.Height = Margin.Top + Margin.Buttom + Border.Height;
        }

        private void MergePosition()
        {
            Margin.Position = Position;
            if (ContentSet.Alignment == AlignmentEnum.Center)
            { Margin.Position.Left += (Console.BufferWidth - 1 - Position.Left - Margin.Width) / 2; }
            else if (ContentSet.Alignment == AlignmentEnum.Right)
            { Margin.Position.Left = Console.BufferWidth - 1 - Margin.Width; }

            Border.Position.Left = Margin.Position.Left + Margin.Left;
            Border.Position.Top = Margin.Position.Top + Margin.Top;

            Padding.Position.Left = Border.Position.Left + Border.LeftMaxLen;
            Padding.Position.Top = Border.Position.Top + Border.TopMaxLen;

            ContentSet.Position.Left = Padding.Position.Left + Padding.Left;
            ContentSet.Position.Top = Padding.Position.Top + Padding.Top;

            // TODO: 各字符串的输出起始位置
            List<Position> Positions = new List<Position>();
            Position Pos = ContentSet.Position;
            Int32 x = 0, y = 0, NowCol = 0, NowRow = 0, LeftCount = 0;
            for (Int32 i = 0; i < ContentSet.Count; i++)
            {
                if (ContentSet.ByColumn)
                { LeftCount = i - ContentSet.Row; NowRow = i % ContentSet.Row + 1; NowCol = (Int32)Math.Ceiling((Double)(i + 1) / ContentSet.Row); }
                else
                { LeftCount = i - 1; NowCol = i % ContentSet.Column + 1; NowRow = (Int32)Math.Ceiling((Double)(i + 1) / ContentSet.Column); }

                x = NowCol == 1 ? Pos.Left : (Positions[LeftCount].Left + ContentSet.ColWidth[NowCol - 2]) + ContentSet.ColIndent[NowCol - 1];
                y = Pos.Top + NowRow - 1;
                Positions.Add(new Position(x, y));
                // Console.WriteLine("Row x Col [{0},{1}]\t [{2},{3}]\tColWidth {4}\tColIndent {5}", NowRow, NowCol, x, y, ContentSet.ColWidth[NowCol - 1], ContentSet.ColIndent[NowCol - 1]);
            }
            Content.Positions = Positions.ToArray();
        }
        #endregion

        // TODO: 整合方法的固定顺序
        public void MergeAll()
        {
            MergeContent();
            MergePadding();
            MergeBorder();
            MergeMargin();
            MergePosition();

            // Console.WriteLine("Margin  L {0},R {1},T {2},B {3}; WxH [{4},{5}];\tPos [{6},{7}]",
            //     Margin.Left, Margin.Right, Margin.Top, Margin.Buttom, Margin.Width, Margin.Height, Margin.Position.Left, Margin.Position.Top);

            // Console.WriteLine("Border  L {0},R {1},T {2},B {3}; WxH [{4},{5}];\tPos [{6},{7}]",
            //     Border.LeftMaxLen, Border.RightMaxLen, Border.TopMaxLen, Border.ButtomMaxLen, Border.Width, Border.Height, Border.Position.Left, Border.Position.Top);
            // Console.WriteLine("{0}{1}{2}\n{3} {4}\n{5}{6}{7}", Border.LeftTop, Border.Top, Border.RightTop, Border.Left, Border.Right, Border.LeftButtom, Border.Buttom, Border.RightButtom);

            // Console.WriteLine("Padding L {0},R {1},T {2},B {3}; WxH [{4},{5}];\tPos [{6},{7}]",
            //     Padding.Left, Padding.Right, Padding.Top, Padding.Buttom, Padding.Width, Padding.Height, Padding.Position.Left, Padding.Position.Top);

            // Console.WriteLine("Content Column {0}, Row {1}; WxH [{2},{3}];\tPos [{4},{5}]",
            //     ContentSet.Column, ContentSet.Row, ContentSet.Width, ContentSet.Height,ContentSet.Position.Left,ContentSet.Position.Top);
            // foreach (Int32 i in ContentSet.ColWidth) { Console.WriteLine(i); }
            // foreach (String i in Content.Flush) { Console.WriteLine("Flush +{0}+", i); }
            // foreach (Int32 i in ContentSet.ColIndent) { Console.WriteLine(i); }
            // foreach (var i in Positions) { Console.SetCursorPosition(i.Left, i.Top); Console.WriteLine("[{0},{1}]", i.Left, i.Top); }
        }
    }

    public class BoxWrite
    {
        public String[] MarginGraph, PaddingGraph, BorderGraph, ContentGraph;
        public BoxSetting Set = new BoxSetting();
        public GraphColorsObj ColorsSet = new GraphColorsObj(true);
        public ReadContentObj ReadContentSet = new ReadContentObj(true);

        #region 内部函数
        // TODO: 外部定义方法
        public void NewSet(BoxSetting newset) { Set = newset; }
        public void NewSet(BoxSetting newset, ReadContentObj readset) { Set = newset; ReadContentSet = readset; }
        public void NewColorSet(Colors margin, Colors padding, Colors border, Colors content, Colors highlight, Colors pointer)
        {
            ColorsSet.Margin = margin;
            ColorsSet.Padding = padding;
            ColorsSet.Border = border;
            ColorsSet.Content = content;
            ColorsSet.HighLight = highlight;
            ColorsSet.Pointer = pointer;
        }

        private void WriteGraph(Position pos, String[] strs, Colors color)
        {
            Int32 Count = 0; Console.ForegroundColor = color.ForegroundColor; Console.BackgroundColor = color.BackgroundColor;
            foreach (String i in strs) { Console.SetCursorPosition(pos.Left, (pos.Top + Count)); Console.WriteLine(i); Count++; }
            Console.ResetColor();
        }
        private void WriteStrXY(Int32 Count, Colors Color, String Pointer, Colors PointerColor)
        {
            String WriteStr = ""; Int32 NowCol = 0;
            NowCol = Set.ContentSet.ByColumn ? (Int32)Math.Ceiling((Double)(Count + 1) / Set.ContentSet.Row) - 1 : Count % Set.ContentSet.Column;

            Console.SetCursorPosition(Set.Content.Positions[Count].Left, Set.Content.Positions[Count].Top);
            Console.ForegroundColor = PointerColor.ForegroundColor; Console.BackgroundColor = PointerColor.BackgroundColor;
            Console.Write(Pointer);

            Console.SetCursorPosition(Set.Content.Positions[Count].Left + Function.RawLength(Pointer), Set.Content.Positions[Count].Top);
            Console.ForegroundColor = Color.ForegroundColor; Console.BackgroundColor = Color.BackgroundColor;
            WriteStr = Set.Content.Strings[Count] + Function.RepeatStr(" ", Set.ContentSet.ColWidth[NowCol] - Function.RawLength(Pointer + Set.Content.Strings[Count]));
            Console.Write(WriteStr); Console.ResetColor();
        }

        private void MergeGraph()
        {
            BorderObj BorderSet = Set.Border;
            Double FixBorder = 0;

            // TODO: Margin 背景区
            MarginGraph = Enumerable.Repeat(Function.RepeatStr(" ", Set.Margin.Width), Set.Margin.Height).ToArray();
            // TODO: Padding 背景区
            PaddingGraph = Enumerable.Repeat(Function.RepeatStr(" ", Set.Padding.Width), Set.Padding.Height).ToArray();
            // TODO: Border 背景区
            BorderGraph = new String[BorderSet.Height];

            Int32 CenterLen = 0, BorderLen = 0, MiddleLen = 0; String MiddleStr;
            // HACK: 手动调整至对齐顶部, 注意区分控制台模式
            if (Set.OldConsole)
            {
                MiddleLen = Function.RawLength(BorderSet.Top);
                CenterLen = (BorderSet.Width - Function.RawLength(BorderSet.LeftTop) - Function.RawLength(BorderSet.RightTop)) / MiddleLen;
                BorderGraph[0] = BorderSet.LeftTop + Function.RepeatStr(BorderSet.Top, CenterLen) + BorderSet.RightTop;

                CenterLen = BorderSet.Width - Function.RawLength(BorderSet.Left) - Function.RawLength(BorderSet.Right);
                MiddleStr = BorderSet.Left + Function.RepeatStr(" ", CenterLen) + BorderSet.Right;
                for (Int32 i = 1; i < BorderSet.Height - 1; i++) { BorderGraph[i] = MiddleStr; }

                MiddleLen = Function.RawLength(BorderSet.Buttom);
                CenterLen = (BorderSet.Width - Function.RawLength(BorderSet.LeftButtom) - Function.RawLength(BorderSet.RightButtom)) / MiddleLen;
                BorderGraph[BorderSet.Height - 1] = BorderSet.LeftButtom + Function.RepeatStr(BorderSet.Buttom, CenterLen) + BorderSet.RightButtom;

                Boolean FixNeed = Function.RawLength(BorderGraph[0]) + Function.RawLength(BorderGraph.Last()) < 2 * BorderSet.Width;
                if (FixNeed)
                {
                    BorderLen = Function.RawLength(BorderSet.Top);
                    FixBorder = BorderSet.Width - Function.RawLength(BorderSet.LeftTop) - Function.RawLength(BorderSet.RightTop);
                    FixBorder = (BorderLen * (Math.Ceiling(FixBorder / BorderLen) + 1) - FixBorder) % BorderLen;
                    FixBorder = (Set.Padding.Right - Set.Padding.Left + FixBorder) % BorderLen + Set.Padding.Left;
                    Console.SetCursorPosition(0, Set.Margin.Position.Top + Set.Margin.Height);
                    Console.WriteLine("[INFO] 建议设置 Padding.Right = {0} + {1}x, x = 0,1,2...", FixBorder, BorderLen);
                }
            }
            else
            {
                MiddleLen = BorderSet.Top.Length;
                CenterLen = (BorderSet.Width - BorderSet.LeftTop.Length - BorderSet.RightTop.Length) / MiddleLen;
                BorderGraph[0] = BorderSet.LeftTop + Function.RepeatStr(BorderSet.Top, CenterLen) + BorderSet.RightTop;

                CenterLen = BorderSet.Width - BorderSet.Left.Length - BorderSet.Right.Length;
                MiddleStr = BorderSet.Left + Function.RepeatStr(" ", CenterLen) + BorderSet.Right;
                for (Int32 i = 1; i < BorderSet.Height - 1; i++) { BorderGraph[i] = MiddleStr; }

                MiddleLen = BorderSet.Buttom.Length;
                CenterLen = (BorderSet.Width - BorderSet.LeftButtom.Length - BorderSet.RightButtom.Length) / MiddleLen;
                BorderGraph[BorderSet.Height - 1] = BorderSet.LeftButtom + Function.RepeatStr(BorderSet.Buttom, CenterLen) + BorderSet.RightButtom;

                Boolean FixNeed = BorderGraph[0].Length + BorderGraph[BorderSet.Height - 1].Length < 2 * BorderSet.Width;
                if (FixNeed)
                {
                    BorderLen = BorderSet.Top.Length; FixBorder = (BorderSet.Width - BorderGraph[0].Length) % BorderLen;
                    Console.SetCursorPosition(0, Set.Margin.Position.Top + Set.Margin.Height);
                    Console.Write("[INFO] 建议设置 Padding.Right = {0} + {1}x, x = 0,1,2...", FixBorder + Set.Padding.Left, BorderLen);
                }
            }

            // TODO: Content 背景区
            ContentGraph = new String[Set.ContentSet.Row];
            Int32 NowCnt = 0, Indent = 0, Differ = 0; String Str;

            for (Int32 Row = 0; Row < Set.ContentSet.Row; Row++)
            {
                Str = "";
                for (Int32 Col = 0; Col < Set.ContentSet.Column; Col++)
                {
                    NowCnt = Set.ContentSet.ByColumn ? Col * Set.ContentSet.Row + Row : Row * Set.ContentSet.Column + Col;
                    Differ = NowCnt < Set.ContentSet.Count ? Set.ContentSet.ColWidth[Col] - Function.RawLength(Set.Content.Strings[NowCnt]) - Function.RawLength(Set.Content.Pointer) : 0;
                    Indent = Set.ContentSet.ColIndent[Col] + (NowCnt < Set.ContentSet.Count ? Function.RawLength(Set.Content.Pointer) : 0);
                    // Console.WriteLine("NowCount {0}, Col {1},Indent {2}", NowCount, Col, Indent);
                    Str += Function.RepeatStr(" ", Indent) + (NowCnt < Set.ContentSet.Count ? Set.Content.Strings[NowCnt] : Set.Content.Flush[Col]) + Function.RepeatStr(" ", Differ);
                }
                ContentGraph[Row] = Str;
            }
        }
        #endregion

        public void WriteBorder()
        {
            MergeGraph();
            WriteGraph(Set.Margin.Position, MarginGraph, ColorsSet.Margin);
            WriteGraph(Set.Border.Position, BorderGraph, ColorsSet.Border);
            WriteGraph(Set.Padding.Position, PaddingGraph, ColorsSet.Padding);
            WriteGraph(Set.ContentSet.Position, ContentGraph, ColorsSet.Content);
            Console.SetCursorPosition(0, Set.Margin.Position.Top + Set.Margin.Height);
        }

        public void ReadContent()
        {
            Int32 OldCount = 0, NowCount = -1; String Key;
            Dictionary<String, Int32[]> BorderIndex = Function.GetBorderIndex(Set.ContentSet.Count, Set.ContentSet.Column, Set.ContentSet.ByColumn);
            Dictionary<String, Int32> ByColumnKeyEvent = new Dictionary<String, Int32>();
            if (Set.ContentSet.ByColumn)
            {
                ByColumnKeyEvent.Add("UpArrow", 1); ByColumnKeyEvent.Add("LeftArrow", Set.ContentSet.Row);
                ByColumnKeyEvent.Add("DownArrow", 1); ByColumnKeyEvent.Add("RightArrow", Set.ContentSet.Row);
            }
            else
            {
                ByColumnKeyEvent.Add("UpArrow", Set.ContentSet.Column); ByColumnKeyEvent.Add("LeftArrow", 1);
                ByColumnKeyEvent.Add("DownArrow", Set.ContentSet.Column); ByColumnKeyEvent.Add("RightArrow", 1);
            }

            // TODO: 方向键选择
            do
            {
                Key = Console.ReadKey().Key.ToString();
                if (NowCount == -1) { if (Key == "Enter") { break; } else { NowCount = 0; goto WriteStr; } }

                if (ReadContentSet.Recycle)
                {
                    if (Key == ReadContentSet.UpArrow)
                    {
                        if (BorderIndex["Top"].Contains(NowCount))
                        { NowCount = BorderIndex["Buttom"][Array.IndexOf(BorderIndex["Top"], NowCount)]; }
                        else { NowCount -= ByColumnKeyEvent["UpArrow"]; }
                    }
                    else if (Key == ReadContentSet.DownArrow)
                    {
                        if (BorderIndex["Buttom"].Contains(NowCount))
                        { NowCount = BorderIndex["Top"][Array.IndexOf(BorderIndex["Buttom"], NowCount)]; }
                        else { NowCount += ByColumnKeyEvent["DownArrow"]; }
                    }
                    else if (Key == ReadContentSet.LeftArrow)
                    {
                        if (BorderIndex["Left"].Contains(NowCount))
                        { NowCount = BorderIndex["Right"][Array.IndexOf(BorderIndex["Left"], NowCount)]; }
                        else { NowCount -= ByColumnKeyEvent["LeftArrow"]; }
                    }
                    else if (Key == ReadContentSet.RightArrow)
                    {
                        if (BorderIndex["Right"].Contains(NowCount))
                        { NowCount = BorderIndex["Left"][Array.IndexOf(BorderIndex["Right"], NowCount)]; }
                        else { NowCount += ByColumnKeyEvent["RightArrow"]; }
                    }
                }
                else
                {
                    if (Key == ReadContentSet.UpArrow) { NowCount -= NowCount - ByColumnKeyEvent["UpArrow"] >= 0 ? ByColumnKeyEvent["UpArrow"] : 0; }
                    else if (Key == ReadContentSet.DownArrow) { NowCount += NowCount + ByColumnKeyEvent["DownArrow"] < Set.ContentSet.Count ? ByColumnKeyEvent["DownArrow"] : 0; }
                    else if (Key == ReadContentSet.LeftArrow) { NowCount -= NowCount - ByColumnKeyEvent["LeftArrow"] >= 0 ? ByColumnKeyEvent["LeftArrow"] : 0; }
                    else if (Key == ReadContentSet.RightArrow) { NowCount += NowCount + ByColumnKeyEvent["RightArrow"] < Set.ContentSet.Count ? ByColumnKeyEvent["RightArrow"] : 0; }
                }

            WriteStr:
                // TODO: 高亮
                WriteStrXY(OldCount, ColorsSet.Content, Function.RepeatStr(" ", Function.RawLength(Set.Content.Pointer)), ColorsSet.Content);
                WriteStrXY(NowCount, ColorsSet.HighLight, Set.Content.Pointer, ColorsSet.Pointer);

                Console.SetCursorPosition(0, Set.Margin.Position.Top + Set.Margin.Height);
                // Console.WriteLine("OldCount {0}, NowCount {1} [{2},{3}]", OldCount, NowCount, Set.Content.Positions[NowCount].Left, Set.Content.Positions[NowCount].Top);
                OldCount = NowCount;
            } while (Key != ConsoleKey.Enter.ToString());
            ReadContentSet.Index = NowCount; ReadContentSet.Value = NowCount == -1 ? null : Set.Content.Strings[NowCount];
        }
    }

    // public class Excute
    // {
    //     public static void Main()
    //     {
    //         BoxSetting Mag = new BoxSetting();

    //         String[] Strings = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
    //         Mag.Content.Strings = Strings;
    //         Mag.Content.Pointer = "> ";
    //         Mag.ContentSet.Column = 3;
    //         // Mag.ContentSet.Row = 3;
    //         // Mag.ContentSet.ColIndent = new Int32[] { 1, 1, 2, 2 };

    //         Mag.Padding.Left = 2;
    //         // Mag.Padding.Right = 3;
    //         Mag.Padding.Top = 1;

    //         // Mag.Border.Left = "│";
    //         // // Mag.Border.Right = "│";
    //         // Mag.Border.Top = "━";
    //         // // Mag.Border.Buttom = "-";
    //         // Mag.Border.LeftTop = "┍";
    //         // Mag.Border.RightTop = "┑";
    //         // Mag.Border.LeftButtom = "┕";
    //         // Mag.Border.RightButtom = "┙";
    //         Mag.Border = new BorderObj(PreBorderEnum.BoldHorizontalLineOnly);

    //         Mag.Margin.Left = Mag.Margin.Top = 1;

    //         Mag.OldConsole = false;
    //         // Mag.ContentSet.ByColumn = true;
    //         // // FIXME: 当按列排序时, 可能会出现不同列数对应同一行数, 这些列数是无效的值
    //         // // HACK:  当列数设置出于无效值时, 可转而设置行数; 或者设置列数至行数改变
    //         Mag.MergeAll();

    //         BoxWrite wt = new BoxWrite();
    //         wt.NewSet(Mag);
    //         // wt.ColorsSet.Margin = new Colors(Console.ForegroundColor, ConsoleColor.DarkMagenta);
    //         wt.ColorsSet.Border = new Colors(ConsoleColor.DarkGray, Console.BackgroundColor);
    //         // wt.ColorsSet.Padding = new Colors(Console.ForegroundColor, ConsoleColor.DarkBlue);
    //         // wt.ColorsSet.Content = new Colors(ConsoleColor.White, ConsoleColor.DarkBlue);
    //         wt.ColorsSet.HighLight = new Colors(ConsoleColor.Yellow, ConsoleColor.DarkBlue);
    //         wt.ColorsSet.Pointer = new Colors(ConsoleColor.Yellow, Console.BackgroundColor);

    //         wt.WriteBorder();
    //         wt.ReadContentSet.Recycle = true;
    //         wt.ReadContent();
    //     }
    // }
}