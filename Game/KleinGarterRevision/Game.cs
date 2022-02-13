using System;
using System.Collections.Generic;
using System.Threading;

namespace KleinGarterRevision
{
    
    //I made the whole game under one class, as i originally thought it would give a slight performance boost.
    //However after some tests i came to the conclusion that the performance was non-differential.
    //Though i've decided to not change anything and leave it as is.

    public class Game
    {
        //Reads data from config
        readonly Config.ConfigData _config = Config.GetConfigData();

        #region Game attributes
        #region Level
        //Level values
        private char _border;
        private int _levelWidth;
        private int _levelHeight;
        private int _levelDifficulty;
        private int _backgroundColor;
        private int _borderColor;
        private bool _hardcore;
        private double _hardcoreLag;
        private int _foodColor;
        #endregion
        
        #region Snake
        //Snake values
        private bool _isAlive = true;
        private int _score;
        private int _foodConsumed; //Sets amount of food the snake consumes
        private char _playerSkin;
        private int _minSpeed; //Sets minimum speed for snake
        private int _maxSpeed; //Sets maximum speed for snake
        private double _speed; //The snakes speed
        private double _speedIncrease; //Finds the appropriate speed increase from the above parameters 
        private double _speedCount; //Counts how much the speed has increased
        private int _bodyColor;
        private int _headColor;
        private ConsoleKey _direction = ConsoleKey.DownArrow; //Starting direction
        #endregion
        
        #region Objects
        //Objects
        private GameObject _head;
        private GameObject _headPrev;
        private readonly Queue<GameObject> _body = new Queue<GameObject>();
        private GameObject _food;
        #endregion
        #endregion
        
        public void RunGame()
        {
            #region Start configuration
            
            #region Level values
            _border = _config.Border;
            _levelWidth = _config.LevelWidth;
            _levelHeight = _config.LevelHeight;
            _levelDifficulty = _config.LevelDifficulty;
            _backgroundColor = _config.BackgroundColor;
            _borderColor = _config.BorderColor;
            _hardcore = _config.Hardcore;
            _hardcoreLag = 0;
            _foodColor = _config.FoodColor;
            #endregion

            #region Player values
            _isAlive = true;
            _score = 0;
            _foodConsumed = _config.FoodConsumed;
            _playerSkin = _config.PlayerSkin;
            _minSpeed = _config.MinSpeed;
            _maxSpeed = _config.MaxSpeed;
            _speed = _minSpeed;
            _speedIncrease = (_maxSpeed - _speed) / ((_levelWidth - 2) * (_levelHeight - 2)) * _levelDifficulty; //Sets appropriate speed increase compared to level and difficulty
            _speedCount = 0;
            _bodyColor = _config.BodyColor;
            _headColor = _config.HeadColor;
            #endregion

            #region Objects
            _food = new GameObject(0, 0);
            _head = new GameObject(0, 0);
            
            //Sets player to center of map
            if ((_levelWidth / 2) % 2 == 0)
                _head.X = (_levelWidth / 2) - 1;
            else
                _head.X = _levelWidth / 2;
            if ((_config.LevelHeight / 2) % 2 == 0)
                _head.Y = (_levelHeight / 2) - 1;
            else
                _head.Y = _levelHeight / 2;
            
            //Queues first body object
            _body.Enqueue(new GameObject(_head.X, _head.Y));
            #endregion
            
            #endregion

            DrawLevel();
            DrawFood();
            
            DateTime nextLoop = DateTime.Now; //Sets timestamp (used to determine speed)

            do
            {
                if (nextLoop <= DateTime.Now) //Appropriate speed or slow = get next loop
                {
                    GetInput();
                    EnactPhysics();

                    nextLoop = nextLoop.AddMilliseconds(1000 / _speed);
                }
                else //If program too fast! = Sleep
                {
                    Thread.Sleep(nextLoop - DateTime.Now);

                    //Hardcore mode makes program fall behind in speed
                    //thus gets to be drawn in the next loop multiple times
                    //as fast as possible making it harder to play visually.
                    if (_hardcore)
                        Thread.Sleep((int) Math.Round(_hardcoreLag));
                }

            } while (_isAlive);
            
            GameOver();
        }
        
        private void GetInput()
        {
            //Gets input if key pressed
            if (Console.KeyAvailable)
            {
                //Sets cursor to an unreachable point on map, to ensure things can't be drawn over
                Console.SetCursorPosition(2, 1);
                
                //Stores pressed key
                ConsoleKey keyPressed = Console.ReadKey().Key;

                //Checks if key is a valid option
                switch (_direction)
                {
                    case ConsoleKey.LeftArrow when keyPressed != ConsoleKey.RightArrow:
                    case ConsoleKey.RightArrow when keyPressed != ConsoleKey.LeftArrow:
                    case ConsoleKey.UpArrow when keyPressed != ConsoleKey.DownArrow:
                    case ConsoleKey.DownArrow when keyPressed != ConsoleKey.UpArrow:
                        _direction = keyPressed;
                        break;

                    case ConsoleKey.Escape:
                        _isAlive = false;
                        break;
                }
            }

            //Saves _head previous position (used for drawing body in another color)
            _headPrev = new GameObject(_head.X, _head.Y);
            
            //Sets new _head position
            switch (_direction)
            {
                case ConsoleKey.LeftArrow:
                    _head.X -= 2;
                    break;
                case ConsoleKey.RightArrow:
                    _head.X += 2;
                    break;
                case ConsoleKey.DownArrow:
                    _head.Y += 1;
                    break;
                case ConsoleKey.UpArrow:
                    _head.Y -= 1;
                    break;
            }
        }
        
        private void EnactPhysics()
        {
            #region Collision detection
            //Checks if snake hit level borders
            if (_head.X <= 0 || _head.X >= _levelWidth || _head.Y <= 0 || _head.Y >= _levelHeight)
            {
                _isAlive = false;
                return;
            }

            //Checks if snake hit body
            foreach(GameObject part in _body)
            {
                if (_head.X == part.X && _head.Y == part.Y)
                {
                    _isAlive = false;
                    return;
                }
            }
            
            //Checks if snake hit food
            if (_head.X == _food.X && _head.Y == _food.Y)
            {
                _foodConsumed++;
                DrawFood();
            }
            #endregion
            
            DrawPlayer();
        }
        
        private void DrawLevel()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i <= _levelHeight; i++)
            {
                for (int j = 0; j <= _levelWidth; j++)
                {
                    if (i == 0 || i == _levelHeight) //Draw border in top and bottom
                    {
                        //Level border
                        Console.ForegroundColor = (ConsoleColor)_borderColor;
                        Console.Write(_border);
                    }
                    else if (j == 0 || j == _levelWidth)//Draw left right border sides
                    {
                        //Level border
                        Console.ForegroundColor = (ConsoleColor)_borderColor;
                        Console.Write(_border);
                    }
                    else //if not border, draw background
                    {
                        //Level Background
                        Console.BackgroundColor = (ConsoleColor)_backgroundColor;
                        Console.Write(' ');
                    }
                }
                Console.Write("\r\n"); 
            }
        }
        
        private void DrawFood()
        {
            bool foodInBadPlace = false;
            do
            {
                Random random = new Random();

                _food.X = random.Next(1, (_levelWidth - 1)); //Generate X within level
                if (_food.X % 2 == 0)
                    _food.X--;

                _food.Y = random.Next(1, (_levelHeight - 1)); //Generate Y within level
                
                foreach (GameObject part in _body) //Checks if food is in body
                {
                    if (part.X == _food.X && part.Y == _food.Y)
                    {
                        foodInBadPlace = true;
                        break;
                    }
                    else
                    {
                        foodInBadPlace = false;
                    }
                }
            } while (foodInBadPlace);

            Console.ForegroundColor = (ConsoleColor) _foodColor;
            Console.SetCursorPosition(_food.X, _food.Y);
            Console.Write('\u25CF');
        }
        
        private void DrawPlayer()
        {
            #region Draw snake

            //Gets random colour for body if _hardcore is true
            if (_hardcore)
            {
                do
                {
                    Random random = new Random();
                    _bodyColor = random.Next(0, 15);
                } while (_bodyColor == _backgroundColor);
            }
            
            //Draws "body" (prev head position)
            Console.ForegroundColor = (ConsoleColor) _bodyColor;
            Console.SetCursorPosition(_headPrev.X, _headPrev.Y);
            Console.Write(_playerSkin);

            #region Food check
            if (_foodConsumed > 0) //if food is hit, don't delete body (extend snake)
            {
                _foodConsumed--;
                _score++;
                _hardcoreLag += (_speedIncrease * 10);

                if (_speed < _maxSpeed)
                {
                    _speed += _speedIncrease;
                    _speedCount += _speedIncrease;
                }
            }
            else //If food isn't hit delete body
            {
                Console.ForegroundColor = (ConsoleColor) _backgroundColor;
                Console.SetCursorPosition(_body.Peek().X, _body.Peek().Y);
                Console.Write(' ');
                _body.Dequeue();
            }
            #endregion

            //Draws snake head
            Console.ForegroundColor = (ConsoleColor)_headColor;
            Console.SetCursorPosition(_head.X, _head.Y);
            Console.Write(_playerSkin);
            Console.SetCursorPosition(_head.X, _head.Y);
            #endregion

            //Add new body part
            _body.Enqueue(new GameObject(_head.X, _head.Y));
        }
        
        private void GameOver()
        {
            #region Animation
            DateTime nextLoop = DateTime.Now; //Sets timestamp (used to determine speed)
            
            //Removes body from level
            do
            {
                if (nextLoop <= DateTime.Now) //Appropriate speed or slow = get next loop
                {
                    Console.ForegroundColor = (ConsoleColor) _backgroundColor;
                    Console.SetCursorPosition(_body.Peek().X, _body.Peek().Y);
                    Console.Write(' ');
                    _body.Dequeue();

                    nextLoop = nextLoop.AddMilliseconds(1000 / (_speed * 5));
                }
                else //If program too fast! = Sleep
                {
                    Thread.Sleep(nextLoop - DateTime.Now);
                }
            } while (_body.Count != 0);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Ded");

            #endregion
            
            Console.ReadLine();
        }
    }
}
