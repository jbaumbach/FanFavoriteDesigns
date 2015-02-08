using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Data
{
    /// <summary>
    /// Class with basic name information, including advanced name parsing functionality.
    /// </summary>
    public class PersonName
    {
        private string _firstName;
        private string _lastName;
        private string _middleInitial = string.Empty;

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

        /// <summary>
        /// Returns the full name of this lead.
        /// </summary>
        /// <returns></returns>
        public string BuildFullName()
        {
            return string.Format("{0} {1}{2}{3}", _firstName, _middleInitial, _middleInitial == string.Empty ? "" : ". ", _lastName);
        }

        public string BuildShortFullName()
        {
            string firstLetter = string.Empty;

            if (_firstName.Length > 1)
            {
                firstLetter = string.Format("{0}. ", _firstName.Substring(0, 1));
            }

            return string.Format("{0}{1}", firstLetter, _lastName);

        }
        /// <summary>
        /// Parses a full name into first, middle, and last and sets the class properties.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <returns>The validity of the full name string.</returns>
        public bool ParseFullName(string fullName)
        {
            return ParseFullName(fullName, false);
        }

        /// <summary>
        /// Parses a full name into first, middle, and last and sets the class properties.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <param name="singleNamesAsFirst">If a single name is found, treat as first name.</param>
        /// <returns>The validity of the full name string.</returns>
        public virtual bool ParseFullName(string fullName, bool singleNamesAsFirst)
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
                string[] names = fullName.Trim().Split(',');

                if (names.Length != 2)
                {
                    //
                    // We have something weird happening
                    //
                }
                else
                {
                    _lastName = names[0].Trim();
                    string temp = names[1].Trim();

                    int tempLength = temp.Length;
                    int lengthFromEndWhereSpaceShouldBe = 2;

                    if (temp.EndsWith("."))
                    {
                        lengthFromEndWhereSpaceShouldBe++;
                    }

                    if ((tempLength > 2) && (temp.Substring(tempLength - lengthFromEndWhereSpaceShouldBe, 1) == " "))
                    {
                        // 
                        // We have a MI
                        //
                        _firstName = temp.Substring(0, tempLength - lengthFromEndWhereSpaceShouldBe);
                        _middleInitial = temp.Substring(tempLength - lengthFromEndWhereSpaceShouldBe + 1, 1);
                    }
                    else
                    {
                        _firstName = temp;
                    }

                    result = true;
                }
            }
            else
            {
                //
                // We have a {first} [mi ]{last} format or
                //  {first} {some middle stuff} {last}  or 
                //  {singlename}  (like "Ronaldo" or some such)
                //
                string[] names = fullName.Split(' ');

                if (names.Length > 0)
                {
                    if (names.Length == 1)
                    {
                        if (singleNamesAsFirst)
                        {
                            _firstName = fullName;
                        }
                        else
                        {
                            _lastName = fullName;
                        }
                    }
                    else if (names.Length == 2)
                    {
                        _firstName = names[0];
                        _lastName = names[1];
                    }
                    else
                    {
                        if (names[1].Replace(".", "").Trim().Length == 1)
                        {
                            _firstName = names[0];
                            _middleInitial = names[1].Substring(0, 1);
                            _lastName = names[names.Length - 1];
                        }
                        else
                        {
                            _firstName = names[0];
                            _lastName = fullName.Substring(fullName.IndexOf(' ') + 1);
                        }
                    }

                    result = true;
                }
            }

            //if (result)
            //{
            //    _jerseyName = _lastName.ToUpper();

            //    if (_jerseyName == string.Empty)
            //    {
            //        _jerseyName = _firstName.ToUpper();
            //    }
            //}

            //
            // Title case our stuff - not tested yet.
            //
            //System.Globalization.TextInfo txt = new System.Globalization.TextInfo();
            //_firstName = txt.ToTitleCase(_firstName);
            //_lastName = txt.ToTitleCase(_lastName);
            //_middleInitial = _middleInitial.ToUpper();


            return result;
        }


    }
}
