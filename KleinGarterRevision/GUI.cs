using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace KleinGarterRevision
{
    //What are you doing in here? Don't even bother looking through this...
    //This is just piles of hardcoded garbage code doing it's job!
    class Gui
    {
        public int InteractionMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t" + @"       ____  __.__         .__             ");
            Console.WriteLine("\t" + @"      |    |/ _|  |   ____ |__| ____       ");
            Console.WriteLine("\t" + @"      |      < |  | _/ __ \|  |/    \      ");
            Console.WriteLine("\t" + @"      |    |  \|  |_\  ___/|  |   |  \     ");
            Console.WriteLine("\t" + @"      |____|__ \____/\___  >__|___|  /     ");
            Console.WriteLine("\t" + @"              \/         \/        \/      ");
            Console.WriteLine("\t" + @"  ________               __                ");
            Console.WriteLine("\t" + @" /  _____/_____ ________/  |_  ___________ ");
            Console.WriteLine("\t" + @"/   \  ___\__  \\_  __ \   __\/ __ \_  __ \");
            Console.WriteLine("\t" + @"\    \_\  \/ __ \|  | \/|  | \  ___/|  | \/");
            Console.WriteLine("\t" + @" \______  (____  /__|   |__|  \___  >__|   ");
            Console.WriteLine("\t" + @"        \/     \/                 \/       ");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n\n\t+------------------+   +------------------+");
            Console.WriteLine("\t|    Start Game    |   |     Settings     |");
            Console.WriteLine("\t+------------------+   +------------------+");

            var direction = Console.ReadKey().Key;

            while (true)
            {
                int interactionId;
                switch (direction)
                {
                    case ConsoleKey.LeftArrow:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(8, 15);
                        Console.WriteLine("+------------------+");
                        Console.SetCursorPosition(8, 16);
                        Console.WriteLine("|    Start Game    |");
                        Console.SetCursorPosition(8, 17);
                        Console.WriteLine("+------------------+");

                        if (Console.KeyAvailable)
                        {
                            direction = Console.ReadKey().Key;

                            if (direction == ConsoleKey.Enter)
                            {
                                Console.Clear();
                                interactionId = 1;
                                return interactionId;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.SetCursorPosition(8, 15);
                                Console.WriteLine("+------------------+");
                                Console.SetCursorPosition(8, 16);
                                Console.WriteLine("|    Start Game    |");
                                Console.SetCursorPosition(8, 17);
                                Console.WriteLine("+------------------+");
                            }
                        }
                        break;


                    case ConsoleKey.RightArrow:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(31, 15);
                        Console.WriteLine("+------------------+");
                        Console.SetCursorPosition(31, 16);
                        Console.WriteLine("|     Settings     |");
                        Console.SetCursorPosition(31, 17);
                        Console.WriteLine("+------------------+");

                        if (Console.KeyAvailable)
                        {
                            direction = Console.ReadKey().Key;

                            if (direction == ConsoleKey.Enter)
                            {
                                Console.Clear();
                                interactionId = 2;
                                return interactionId;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.SetCursorPosition(31, 15);
                                Console.WriteLine("+------------------+");
                                Console.SetCursorPosition(31, 16);
                                Console.WriteLine("|     Settings     |");
                                Console.SetCursorPosition(31, 17);
                                Console.WriteLine("+------------------+");
                            }
                        }
                        break;

                    default:
                        if (Console.KeyAvailable)
                        {
                            direction = Console.ReadKey().Key;
                        }
                        break;
                }
            }
        }

        //Used for settings menu
        private readonly List<string> _setting = new List<string>();
        private readonly List<string> _data = new List<string>();

        public void SettingsMenu()
        {
            Config.ConfigData config = Config.GetConfigData(); //Gets config data

            if (_setting.Count == 0)
            {
                //Adds settings
                #region Settings values
                _setting.Add("Hardcore");
                _setting.Add("FoodConsumed");
                _setting.Add("LevelWidth");
                _setting.Add("LevelHeight");
                _setting.Add("LevelDifficulty");
                _setting.Add("HeadColor");
                _setting.Add("BodyColor");
                _setting.Add("FoodColor");
                _setting.Add("BorderColor");
                _setting.Add("BackgroundColor");
                #endregion
                
                //Adds data to settings
                #region Data values
                _data.Add(Convert.ToString("<" + config.Hardcore + ">"));
                _data.Add(Convert.ToString("<" + config.FoodConsumed + ">"));
                _data.Add(Convert.ToString("<" + config.LevelWidth + ">"));
                _data.Add(Convert.ToString("<" + config.LevelHeight + ">"));
                _data.Add(Convert.ToString("<" + config.LevelDifficulty + ">"));
                _data.Add(Convert.ToString("<" + config.HeadColor + ">"));
                _data.Add(Convert.ToString("<" + config.BodyColor + ">"));
                _data.Add(Convert.ToString("<" + config.FoodColor + ">"));
                _data.Add(Convert.ToString("<" + config.BorderColor + ">"));
                _data.Add(Convert.ToString("<" + config.BackgroundColor + ">"));
                #endregion
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t" + @"  _________       __    __  .__                      ");
            Console.WriteLine("\t" + @" /   _____/ _____/  |__/  |_|__| ____    ____  ______");
            Console.WriteLine("\t" + @" \_____  \_/ __ \   __\   __\  |/    \  / ___\/  ___/");
            Console.WriteLine("\t" + @" /        \  ___/|  |  |  | |  |   |  \/ /_/  >___ \ ");
            Console.WriteLine("\t" + @"/_______  /\___  >__|  |__| |__|___|  /\___  /____  >");
            Console.WriteLine("\t" + @"        \/     \/                   \//_____/     \/ ");
            Console.WriteLine("\t" + @"-----------------------------------------------------");

            //Writes out settings
            for (int i = 0; i < _setting.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                WriteSetting(i);
            }

            Console.WriteLine("\n\n");
            Console.WriteLine("ESC = EXIT");
            Console.WriteLine("ENTER = SAVES CONFIG");
            Console.WriteLine("TAB = DEFAULT CONFIG");
            
            int tmp; //Used to get users previously position (used to color setting green again)
            int interactionId = 0; //Used to see what setting the user is hovering over
            ConsoleKey direction = ConsoleKey.UpArrow;

            #region Highlights start position
            Console.ForegroundColor = ConsoleColor.White;
            WriteSetting(interactionId);
            #endregion

            //Navigation in settings
            while (direction != ConsoleKey.Escape)
            {
                if (Console.KeyAvailable)
                {
                    direction = Console.ReadKey().Key;

                    //Switch for settings menu. Long and boring, but it works...
                    switch (direction)
                    {
                        case ConsoleKey.LeftArrow:
                            switch (interactionId)
                            {
                                case 0:
                                    config.Hardcore = false;
                                    _data[interactionId] = "<False>";

                                    WriteSetting(interactionId);
                                    break;
                                case 1:
                                    if (config.FoodConsumed > 0)
                                        config.FoodConsumed--;

                                    _data[interactionId] = $"<{config.FoodConsumed}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 2:
                                    if (config.LevelWidth > 8)
                                        config.LevelWidth -= 2;

                                    _data[interactionId] = $"<{config.LevelWidth}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 3:
                                    if (config.LevelHeight > 4)
                                        config.LevelHeight--;

                                    _data[interactionId] = $"<{config.LevelHeight}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 4:
                                    if (config.LevelDifficulty > 0)
                                        config.LevelDifficulty--;

                                    _data[interactionId] = $"<{config.LevelDifficulty}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 5:
                                    if (config.HeadColor > 0)
                                        config.HeadColor--;

                                    _data[interactionId] = $"<{config.HeadColor}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 6:
                                    if (config.BodyColor > 0)
                                        config.BodyColor--;

                                    _data[interactionId] = $"<{config.BodyColor}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 7:
                                    if (config.FoodColor > 0)
                                        config.FoodColor--;

                                    _data[interactionId] = $"<{config.FoodColor}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 8:
                                    if (config.BorderColor > 0)
                                        config.BorderColor--;

                                    _data[interactionId] = $"<{config.BorderColor}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 9:
                                    if (config.BackgroundColor > 0)
                                        config.BackgroundColor--;

                                    _data[interactionId] = $"<{config.BackgroundColor}>";

                                    WriteSetting(interactionId);
                                    break;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            switch (interactionId)
                            {
                                case 0:
                                    config.Hardcore = true;
                                    _data[interactionId] = "<True>";

                                    WriteSetting(interactionId);
                                    break;
                                case 1:
                                    if (config.FoodConsumed < (config.LevelWidth * config.LevelHeight))
                                        config.FoodConsumed++;

                                    _data[interactionId] = $"<{config.FoodConsumed}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 2:
                                        config.LevelWidth += 2;

                                    _data[interactionId] = $"<{config.LevelWidth}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 3:
                                        config.LevelHeight++;

                                    _data[interactionId] = $"<{config.LevelHeight}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 4:
                                    if (config.LevelDifficulty < 10)
                                        config.LevelDifficulty++;

                                    _data[interactionId] = $"<{config.LevelDifficulty}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 5:
                                    if (config.HeadColor < 15)
                                        config.HeadColor++;

                                    _data[interactionId] = $"<{config.HeadColor}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 6:
                                    if (config.BodyColor < 15)
                                        config.BodyColor++;

                                    _data[interactionId] = $"<{config.BodyColor}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 7:
                                    if (config.FoodColor < 15)
                                        config.FoodColor++;

                                    _data[interactionId] = $"<{config.FoodColor}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 8:
                                    if (config.BorderColor < 15)
                                        config.BorderColor++;

                                    _data[interactionId] = $"<{config.BorderColor}>";

                                    WriteSetting(interactionId);
                                    break;
                                case 9:
                                    if (config.BackgroundColor < 15)
                                        config.BackgroundColor++;

                                    _data[interactionId] = $"<{config.BackgroundColor}>";

                                    WriteSetting(interactionId);
                                    break;
                            }
                            break;
                        case ConsoleKey.DownArrow when interactionId <= _setting.Count - 1:
                            tmp = interactionId;
                            if(interactionId != _setting.Count - 1)
                                interactionId++;

                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            WriteSetting(tmp);

                            Console.ForegroundColor = ConsoleColor.White;
                            WriteSetting(interactionId);
                            break;
                        case ConsoleKey.UpArrow when interactionId >= 0:
                            tmp = interactionId;
                            if (interactionId != 0)
                                interactionId--;

                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            WriteSetting(tmp);

                            Console.ForegroundColor = ConsoleColor.White;
                            WriteSetting(interactionId);
                            break;
                        case ConsoleKey.Tab:
                            config.Hardcore = false;
                            config.FoodColor = 4;
                            config.HeadColor = 0;
                            config.BodyColor = 6;
                            config.PlayerSkin = '\u25A0';
                            config.FoodConsumed = 0;
                            config.MinSpeed = 10;
                            config.MaxSpeed = 40;
                            config.BackgroundColor = 14;
                            config.BorderColor = 8;
                            config.Border = '\u2588';
                            config.LevelWidth = 50;
                            config.LevelHeight = 25;
                            config.LevelDifficulty = 3;

                            Config.SaveConfigData(config);
                            break;
                        case ConsoleKey.Enter:
                            Config.SaveConfigData(config);
                            break;
                    }
                }
            }
        }

        private void WriteSetting(int id)
        {
            Console.SetCursorPosition(8, id + 8);
            Console.Write($"{_setting[id]}:");
            Console.SetCursorPosition(51, id + 8);
            Console.Write(_data[id].PadLeft(10));
        }
    }
}
