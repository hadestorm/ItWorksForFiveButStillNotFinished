using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flow
{
    struct FrameLine
    {
        public string topLeft;
        public string topRight;
        public string bottomLeft;
        public string bottomRight;
        public string lineX;
        public string lineY;

        public FrameLine(int n)
        {
            topLeft = "┌";
            topRight = "┐";
            bottomLeft = "└";
            bottomRight = "┘";
            lineX = "─";
            lineY = "│";
        }
    }

    struct FrameDoubleLine
    {
        public string topLeft;
        public string topRight;
        public string bottomLeft;
        public string bottomRight;
        public string lineA;
        public string lineB;

        public FrameDoubleLine(int n)
        {
            topLeft = "╔";
            topRight = "╗";
            bottomLeft = "╚";
            bottomRight = "╝";
            lineA = "═";
            lineB = "║";
        }
    }

    struct Square
    {
        public string model1;
        public string model2;
        public string model3;
        public string model4;
        public string model5;

        public Square(int n)
        {
            model1 = "■";
            model2 = "█";
            model3 = "▓";
            model4 = "▒";
            model5 = "░";
        }
    }

    struct HorizontalLine
    {
        public string left;
        public string right;
        public string line;
        public string cross;

        public HorizontalLine(int n)
        {
            left = "├";
            right = "┤";
            line = "─";
            cross = "┼";
        }
    }

    struct HorizontalLineDouble
    {
        public string left;
        public string right;
        public string line;
        public string cross;

        public HorizontalLineDouble(int n)
        {
            left = "╠";
            right = "╣";
            line = "═";
            cross = "╬";
        }
    }

    struct VerticalLine
    {
        public string top;
        public string bottom;
        public string line;
        public string cross;

        public VerticalLine(int n)
        {
            top = "┬";
            bottom = "┴";
            line = "│";
            cross = "┼";
        }
    }

    struct VerticalLineDouble
    {
        public string top;
        public string bottom;
        public string line;
        public string cross;

        public VerticalLineDouble(int n)
        {
            top = "╦";
            bottom = "╩";
            line = "║";
            cross = "╬";
        }
    }

    // TODO: popups for type kind shit
    static class PsCon
    {
        public static void PrintFrameLine(int positionX, int positionY, int sizeX, int sizeY, ConsoleColor text, ConsoleColor background)
        {
            int SizeX = positionX + sizeX;
            int SizeY = positionY + sizeY;
            FrameLine f = new FrameLine(0);

            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            for (int y = positionY; y < SizeY; y++)
            {
                for (int x = positionX; x < SizeX; x++)
                {
                    if (y == positionY && x == positionX)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.topLeft);
                    }

                    if (y == positionY && x > positionX && x < SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.lineX);
                    }

                    if (y == positionY && x == SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.topRight);
                    }

                    if (y > positionY && y < SizeY - 1 && x == positionX || y > positionY && y < SizeY - 1 && x == SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.lineY);
                    }

                    if (y == SizeY - 1 && x == positionX)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.bottomLeft);
                    }

                    if (y == SizeY - 1 && x > positionX && x < SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.lineX);
                    }

                    if (y == SizeY - 1 && x == SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.bottomRight);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void PrintFrameDoubleLine(int positionX, int positionY, int sizeX, int sizeY, ConsoleColor text, ConsoleColor background)
        {
            int SizeY = positionY + sizeY;
            int SizeX = positionX + sizeX;

            FrameDoubleLine f = new FrameDoubleLine(0);
            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            for (int y = positionY; y < SizeY; y++)
            {
                for (int x = positionX; x < SizeX; x++)
                {
                    if (y == positionY && x == positionX)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.topLeft);
                    }
                    if (y == positionY && x > positionX && x < SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.lineA);
                    }
                    if (y == positionY && x == SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.topRight);
                    }
                    if (y > positionY && y < SizeY - 1 && x == positionX || y > positionY && y < SizeY - 1 && x == SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.lineB);
                    }
                    if (y == SizeY - 1 && x == positionX)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.bottomLeft);
                    }
                    if (y == SizeY - 1 && x > positionX && x < SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.lineA);
                    }
                    if (y == SizeY - 1 && x == SizeX - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(f.bottomRight);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void PrintString(string str, int X, int Y, ConsoleColor text, ConsoleColor background)
        {
            Console.ForegroundColor = text;
            Console.BackgroundColor = background;

            Console.SetCursorPosition(X, Y);
            Console.Write(str);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}