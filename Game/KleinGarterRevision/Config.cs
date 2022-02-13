using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace KleinGarterRevision
{
    public class Config
    {
        #region Default Data
        //Player attributes
        private const int DEF_FOOD_COLOR = 4;
        private const int DEF_HEAD_COLOR = 0;
        private const int DEF_BODY_COLOR = 6;
        private const char DEF_PLAYER_SKIN = '\u25A0';
        private const int DEF_FOOD_CONSUMED = 50;
        private const int DEF_MIN_SPEED = 10;
        private const int DEF_MAX_SPEED = 40;
        private const bool DEF_HARDCORE = false;

        //Level attributes
        private const int DEF_BACKGROUND_COLOR = 14;
        private const int DEF_BORDER_COLOR = 8;
        private const char DEF_BORDER = '\u2588';
        private const int DEF_LEVEL_WIDTH = 50;
        private const int DEF_LEVEL_HEIGHT = 25;
        private const int DEF_LEVEL_DIFFICULTY = 1;
        #endregion

        //Name of .xml file
        private static string CONFIG_FNAME = "config.xml";

        public static ConfigData GetConfigData()
        {
            if (!File.Exists(CONFIG_FNAME)) //Creates config file with default values
            {
                using (FileStream fs = new FileStream(CONFIG_FNAME, FileMode.Create))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(ConfigData));
                    ConfigData sxml = new ConfigData();
                    xs.Serialize(fs, sxml);
                    return sxml;
                }
            }
            else //Reads config from file
            {
                using (FileStream fs = new FileStream(CONFIG_FNAME, FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(ConfigData));
                    ConfigData sc = (ConfigData)xs.Deserialize(fs);
                    return sc;
                }
            }
        }

        //Saves config data to file
        public static bool SaveConfigData(ConfigData config)
        {
            if (!File.Exists(CONFIG_FNAME)) return false;

            using (FileStream fs = new FileStream(CONFIG_FNAME, FileMode.Truncate))
            {
                XmlSerializer xs = new XmlSerializer(typeof(ConfigData));
                xs.Serialize(fs, config);
                return true;
            }
        }

        //Stored config data
        public class ConfigData
        {
            //Player attributes
            public int FoodColor;
            public int HeadColor;
            public int BodyColor;
            public char PlayerSkin;
            public int FoodConsumed;
            public int MinSpeed;
            public int MaxSpeed;
            public bool Hardcore;

            //Level attributes
            public int BackgroundColor;
            public int BorderColor;
            public char Border;
            public int LevelWidth;
            public int LevelHeight;
            public int LevelDifficulty;

            public ConfigData()
            {
                FoodColor = DEF_FOOD_COLOR;
                HeadColor = DEF_HEAD_COLOR;
                BodyColor = DEF_BODY_COLOR;
                PlayerSkin = DEF_PLAYER_SKIN;
                FoodConsumed = DEF_FOOD_CONSUMED;
                MinSpeed = DEF_MIN_SPEED;
                MaxSpeed = DEF_MAX_SPEED;
                Hardcore = DEF_HARDCORE;

                BackgroundColor = DEF_BACKGROUND_COLOR;
                BorderColor = DEF_BORDER_COLOR;
                Border = DEF_BORDER;
                LevelWidth = DEF_LEVEL_WIDTH;
                LevelHeight = DEF_LEVEL_HEIGHT;
                LevelDifficulty = DEF_LEVEL_DIFFICULTY;
            }
        }
    }
}
