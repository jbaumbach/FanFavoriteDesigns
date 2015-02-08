using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Data
{
    public class Player
    {
        private int _playerCode;
        private string _firstName;
        private string _lastName;
        private int _number;
        private string _middleInitial;

        public int PlayerCode
        {
            get { return _playerCode; }
            set { _playerCode = value; }
        }

        public string FullName
        {
            get { return BuildFullName(); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string MiddleInitial
        {
            get { return _middleInitial; }
            set { _middleInitial = value; }
        }

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }
        private string _jerseyName;

        public string JerseyName
        {
            get { return _jerseyName; }
            set { _jerseyName = value; }
        }

        private string BuildFullName()
        {
            return string.Format("{0}{1}{2} {3}", _firstName, _middleInitial, _middleInitial == string.Empty ? "" : ".", _lastName);
        }

        /// <summary>
        /// Parses a full name into first, middle, and last and sets the class properties.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <returns>The validity of the full name string.</returns>
        public bool ParseFullName(string fullName)
        {
            bool result = false;
            _firstName = string.Empty;
            _middleInitial = string.Empty;
            _lastName = string.Empty;

            if (fullName.Contains(","))
            {
                //
                // We have a {last}, {first} [mi] format
                //
                string[] names = fullName.Split(' ');

                if (names.Length > 0)
                {
                    if (names.Length == 1)
                    {
                        _lastName = fullName;
                    }
                    else if (names.Length == 2)
                    {
                        _lastName = names[0].Replace(",", "");
                        _firstName = names[1].Replace(",", "");
                    }
                    else
                    {
                        _lastName = names[0].Replace(",", "");
                        _firstName = names[1].Replace(",", "");
                        _middleInitial = names[2].Replace(",", "").Substring(0, 1);
                    }

                    result = true;
                }
            }
            else
            {
                //
                // We have a {first} [mi ]{last} format
                //
                string[] names = fullName.Split(' ');

                if (names.Length > 0)
                {
                    if (names.Length == 1)
                    {
                        _lastName = fullName;
                    }
                    else if (names.Length == 2)
                    {
                        _firstName = names[0];
                        _lastName = names[1];
                    }
                    else
                    {
                        _firstName = names[0];
                        _middleInitial = names[1].Substring(0, 1);
                        _lastName = names[names.Length - 1];
                    }

                    result = true;
                }
            }

            if (result)
            {
                _jerseyName = _lastName.ToUpper();
            }

            return result;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Player()
        {
        }

        /// <summary>
        /// Constructor with automatic full name parsing.
        /// </summary>
        /// <param name="fullName">The player's full name.</param>
        /// <param name="number">The player's number.</param>
        public Player(string fullName, int number) : this()
        {
            ParseFullName(fullName);
            _number = number;
        }
    }
}
