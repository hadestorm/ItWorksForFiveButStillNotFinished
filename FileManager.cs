using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Flow
{
    class Flow
    {
        public static int HEIGHT_KEYS = 3;
        public static int BOTTOM_OFFSET = 2;

        public event OnKey KeyPress;
        List<FilePanel> panels = new List<FilePanel>();
        private int activePanelIndex;

        static Flow()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(120, 41);
            Console.SetBufferSize(120, 41);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public Flow()
        {
            FilePanel filePanel = new FilePanel();
            filePanel.Top = 0;
            filePanel.Left = 0;
            this.panels.Add(filePanel);

            filePanel = new FilePanel();
            filePanel.Top = FilePanel.PANEL_HEIGHT;
            filePanel.Left = 0;
            this.panels.Add(filePanel);

            activePanelIndex = 0;

            this.panels[this.activePanelIndex].Active = true;
            KeyPress += this.panels[this.activePanelIndex].KeyboardProcessing;

            foreach (FilePanel fp in panels)
            {
                fp.Show();
            }

            this.ShowKeys();
        }

        public void Explorer()
        {
            bool exit = false;
            while (!exit)
            {
                if (Console.KeyAvailable)
                {
                    this.ClearMessage();

                    ConsoleKeyInfo userKey = Console.ReadKey(true);
                    switch (userKey.Key)
                    {
                        case ConsoleKey.Tab:
                            this.ChangeActivePanel();
                            break;
                        case ConsoleKey.Enter:
                            this.ChangeDirectoryOrRunProcess();
                            break;
                        case ConsoleKey.F1:
                            this.CreateDirectory();
                            break;
                        case ConsoleKey.F2:
                            this.CreateFile();
                            break;
                        case ConsoleKey.F3:
                            this.Delete();
                            break;
                        case ConsoleKey.F10:
                            exit = true;
                            Console.ResetColor();
                            Console.Clear();
                            break;
                        case ConsoleKey.DownArrow:
                            goto case ConsoleKey.UpArrow;
                        case ConsoleKey.UpArrow:
                            this.KeyPress(userKey);
                            break;
                    }
                }
            }
        }

        private string AskName(string message)
        {
            string name;
            Console.CursorVisible = true;
            do
            {
                this.ClearMessage();
                this.ShowMessage(message);
                name = Console.ReadLine();
            } while (name.Length == 0);
            Console.CursorVisible = false;
            this.ClearMessage();
            return name;
        }

        private void Delete()
        {
            if (this.panels[this.activePanelIndex].isDiscs)
            {
                return;
            }

            FileSystemInfo fileObject = this.panels[this.activePanelIndex].GetActiveObject();
            try
            {
                if (fileObject is DirectoryInfo)
                {
                    ((DirectoryInfo)fileObject).Delete(true);
                }
                else
                {
                    ((FileInfo)fileObject).Delete();
                }
                this.RefreshPanels();
            }
            catch (Exception e)
            {
                this.ShowMessage(e.Message);
                return;
            }
        }

        private void CreateDirectory()
        {
            string destinationPath = this.panels[this.activePanelIndex].Path;
            string directoryName = this.AskName("Введите название директории: ");
            
            string dirFullName = Path.Combine(destinationPath, directoryName);
            DirectoryInfo dir = new DirectoryInfo(dirFullName);
            if (!dir.Exists)
            {
                dir.Create();
            }

            this.RefreshPanels();
        }

        private void CreateFile()
        {
            string destPath = this.panels[this.activePanelIndex].Path;
            string fileName = this.AskName("Введите имя файла: ");

            string dirFullName = Path.Combine("@", destPath, fileName);
  
            using (FileStream fs = File.Create(dirFullName))
  
            this.RefreshPanels();
        }

        private void RefreshPanels()
        {
            if (this.panels == null || this.panels.Count == 0)
            {
                return;
            }

            foreach (FilePanel panel in panels)
            {
                if (!panel.isDiscs)
                {
                    panel.UpdateContent(true);
                }
            }
        }

        private void ChangeActivePanel()
        {
            this.panels[this.activePanelIndex].Active = false;
            KeyPress -= this.panels[this.activePanelIndex].KeyboardProcessing;
            this.panels[this.activePanelIndex].UpdateContent(false);

            this.activePanelIndex++;
            if (this.activePanelIndex >= this.panels.Count)
            {
                this.activePanelIndex = 0;
            }

            this.panels[this.activePanelIndex].Active = true;
            KeyPress += this.panels[this.activePanelIndex].KeyboardProcessing;
            this.panels[this.activePanelIndex].UpdateContent(false);
        }

        private void ChangeDirectoryOrRunProcess()
        {
            FileSystemInfo fsInfo = this.panels[this.activePanelIndex].GetActiveObject();
            if (fsInfo != null)
            {
                if (fsInfo is DirectoryInfo)
                {
                    try
                    {
                        Directory.GetDirectories(fsInfo.FullName);
                    }
                    catch
                    {
                        return;
                    }

                    this.panels[this.activePanelIndex].Path = fsInfo.FullName;
                    this.panels[this.activePanelIndex].SetLists();
                    this.panels[this.activePanelIndex].UpdatePanel();
                }
                else
                {
                    Process.Start(((FileInfo)fsInfo).FullName);
                }
            }
            else
            {
                string currentPath = this.panels[this.activePanelIndex].Path;
                DirectoryInfo currentDirectory = new DirectoryInfo(currentPath);
                DirectoryInfo upLevelDirectory = currentDirectory.Parent;

                if (upLevelDirectory != null)
                {
                    this.panels[this.activePanelIndex].Path = upLevelDirectory.FullName;
                    this.panels[this.activePanelIndex].SetLists();
                    this.panels[this.activePanelIndex].UpdatePanel();
                }

                else
                {
                    this.panels[this.activePanelIndex].SetDiscs();
                    this.panels[this.activePanelIndex].UpdatePanel();
                }
            }
        }

        private void ShowKeys()
        {
            string[] menu = { "F1 MKDIR", " F2 TOUCH", "F3 DELETE" };

            int cellLeft = this.panels[0].Left;
            int cellTop = FilePanel.PANEL_HEIGHT * this.panels.Count;
            int cellWidth = FilePanel.PANEL_WIDTH / menu.Length;
            int cellHeight = Flow.HEIGHT_KEYS;

            for (int i = 0; i < menu.Length; i++)
            {
                PsCon.PrintFrameLine(cellLeft + i * cellWidth, cellTop, cellWidth, cellHeight, ConsoleColor.White, ConsoleColor.Black);
                PsCon.PrintString(menu[i], cellLeft + i * cellWidth + 1, cellTop + 1, ConsoleColor.White, ConsoleColor.Black);
            }
        }
       
        private void ShowMessage(string message)
        {
            PsCon.PrintString(message, 0, Console.WindowHeight - BOTTOM_OFFSET, ConsoleColor.White, ConsoleColor.Black);
        }

        private void ClearMessage()
        {
            PsCon.PrintString(new String(' ', Console.WindowWidth), 0, Console.WindowHeight - BOTTOM_OFFSET, ConsoleColor.White, ConsoleColor.Black);
        }
    }
}