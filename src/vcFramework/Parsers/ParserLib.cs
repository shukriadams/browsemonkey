//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace vcFramework.Parsers
{
    /// <summary> 
    /// Static class library of various string manipulation methods. Note : contains
    /// some very old code that is slowly being refactored or cleaned out. Yes, there be
    /// Hungarian dragons here.
    /// </summary>
    public class ParserLib
    {

        /// <summary>
        /// Pads a string out with a substring until a length is reached
        /// </summary>
        /// <param name="s"></param>
        /// <param name="padder"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string PadLeft(string s, string padder, int length)
        {
            while (s.Length < length)
                s = padder + s;
            return s;
        }

        /// <summary>
        /// Pads a string out with a substring until a length is reached
        /// </summary>
        /// <param name="s"></param>
        /// <param name="padder"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string PadRight(string s, string padder, int length)
        {
            while (s.Length < length)
                s = s + padder;
            return s;
        }

        /// <summary> Clips the given number of characters from the end of a string</summary>
        /// <param name="main"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string ClipFromEnd(
            string main,
            int amount
            )
        {
            // returns blank string if invalid clip length given
            if (amount >= main.Length)
                return string.Empty;

            return main.Substring(
                0,
                main.Length - amount);
        }



        /// <summary> Clips the given number of characters from the front of a string </summary>
        /// <param name="main"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string ClipFromStart(
            string main,
            int amount
            )
        {
            if (amount >= main.Length)
                return string.Empty;

            return main.Substring(
                amount,
                main.Length - amount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="main"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string ReturnFromStart(
            string main,
            int amount
            )
        {
            if (amount >= main.Length)
                return main;

            return main.Substring(
                0,
                amount);
        }

        /// <summary> Splits a string at a specified substring </summary>
        /// <param name="main"></param>
        /// <param name="thedivider"></param>
        /// <param name="whichway"></param>
        /// <param name="intSwitch"></param>
        /// <returns></returns>
        public static string DivideString(
            string main,
            string thedivider,
            int whichway,
            int intSwitch
            )
        {
            // cuts a string into two parts based on the occurence of a substring. the switch enables the divider to be discarded, or included in one of the resulting strings
            // whichway settings : 1 = function outputs first half of splice, 2 = function outputs second half of splice
            // switch settings: 0 = discard thedivider entirely, 1 = include thedivider on first_string, 2 = include thedivider on second_string
            string first_string = "";
            string second_string = "";
            string output = "";
            int thedivider_length;
            int mainstring_length;
            bool error = false;

            mainstring_length = main.Length;
            thedivider_length = thedivider.Length;

            if ((mainstring_length == 0) || (thedivider_length == 0))
                error = true;
            if (main.IndexOf(thedivider) == -1)
                error = true;

            if (!error)
            {
                int first_string_length;
                switch (intSwitch)
                {
                    case 0:
                        first_string_length = IndexOfFixed(main, thedivider);
                        first_string = main.Substring(0, first_string_length);
                        second_string = main.Substring(first_string_length + thedivider_length, mainstring_length - thedivider_length - first_string_length);
                        break;
                    case 1:
                        first_string_length = IndexOfFixed(main, thedivider) + thedivider_length;
                        first_string = main.Substring(0, first_string_length);
                        second_string = main.Substring(first_string_length, mainstring_length - first_string_length);
                        break;
                    case 2:
                        first_string_length = IndexOfFixed(main, thedivider);
                        first_string = main.Substring(0, first_string_length);
                        second_string = main.Substring(first_string_length, mainstring_length - first_string_length);
                        break;
                    default:
                        output = "Incorrect switch assignment on divide_string.";
                        error = true;
                        break;
                }

                if (!error)
                {
                    switch (whichway)
                    {
                        case 1:
                            output = first_string;
                            break;
                        case 2:
                            output = second_string;
                            break;
                        default:
                            output = "Incorrect :ww: assignment on divide_string.";
                            break;
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Replaces the text at the given location with a substring
        /// </summary>
        /// <param name="s"></param>
        /// <param name="position"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static string Replace(string main, int position, string sub)
        {
            string result = string.Empty;

            // pre
            if (position > 0)
                result = main.Substring(0, position);

            // replace
            result += sub;

            // trail
            if (main.Length > position + sub.Length)
                result += main.Substring(position + sub.Length, main.Length - position - sub.Length);

            return result;
        }

        /// <summary>
        /// Capitalizes the first Alpabetic character in string. If first character is numeric, no change is made.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CapitalizeFirst(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                string s = input.Substring(i, 1);
                if (StringTypeTestLib.IsAlphanumeric(s))
                {
                    input = ParserLib.Replace(input, i, s.ToUpper());
                    return input;
                }
            }
            return input;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CapitalizeFirstOfAll(string input)
        {
            const string pattern = @"\b(\w|['-])+\b";
            string result = Regex.Replace(
                input, pattern,
                m => m.Value[0].ToString().ToUpper() + m.Value.Substring(1));

            return result;
        }

        /// <summary>
        /// Lower cases the first Alpabetic character in string. If first character is numeric, no change is made.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string LowerCaseFirst(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                string s = input.Substring(i, 1);
                if (StringTypeTestLib.IsAlphanumeric(s))
                {
                    input = ParserLib.Replace(input, i, s.ToLower());
                    return input;
                }
            }
            return input;
        }

        /// <summary> 
        /// Doubles single parenths in string to make string SQL-friendly. Also, the very first function I ever wrote in my life.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Fixit(
            string input
            )
        {
            return input.Replace("'", "''");
        }


        /// <summary> 
        /// Written to replace the standard .IndexOf String method 
        /// </summary>
        /// <param name="main"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static int IndexOfFixed(
            string main,
            string sub
            )
        {
            if (main.IndexOf(sub) == -1)
                return -1;

            if (sub.Length == 1)
                return main.IndexOf(Convert.ToChar(sub));

            return main.IndexOf(sub);
        }



        /// <summary> 
        /// Written to replace the standard .IndexOf String method 
        /// </summary>
        /// <param name="main"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static int IndexOfFixed(
            string main,
            string sub,
            int startPosition
            )
        {
            if (main.IndexOf(sub, startPosition) == -1)
                return -1;

            if (sub.Length == 1)
                return main.IndexOf(Convert.ToChar(sub), startPosition);
            return main.IndexOf(sub, startPosition);
        }

        /// <summary> 
        /// Written to replace the standard .IndexOf String method 
        /// </summary>
        /// <param name="main"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static int IndexOfFixed(
            string main,
            string sub,
            int startPosition,
            int intCount
            )
        {
            int intOutput;
            int i;
            int intSearchCurrentPosition = 0;
            int intLastConfirmedPosition = 0;

            if (main.IndexOf(sub, startPosition) == -1)
                return -1;


            intSearchCurrentPosition = startPosition;
            for (i = 0; i < intCount; i++)
            {
                if (sub.Length == 1)
                {
                    intLastConfirmedPosition = main.IndexOf(Convert.ToChar(sub), intSearchCurrentPosition);
                    intSearchCurrentPosition = intLastConfirmedPosition + sub.Length;
                }
                else
                {
                    intLastConfirmedPosition = main.IndexOf(sub, intSearchCurrentPosition);
                    intSearchCurrentPosition = intLastConfirmedPosition + sub.Length;

                }
            }
            intOutput = intLastConfirmedPosition;


            return intOutput;
        }

        /// <summary>
        /// Merges two strings where they are common
        /// </summary>
        /// <param name="leading"></param>
        /// <param name="trailing"></param>
        /// <returns></returns>
        public static string Merge(
            string leading,
            string trailing
            )
        {
            int position = 0;

            while (position < leading.Length)
            {
                position++;

                string test = leading.Substring(
                    leading.Length - position,
                    position);

                if (trailing.StartsWith(test))
                    break;
            }

            if (position >= leading.Length)
                return String.Empty;

            return leading.Substring(0, leading.Length - position) + trailing;
        }

        /// <summary>
        /// Returns true if  query occurs after point in main.
        /// </summary>
        /// <param name="main"></param>
        /// <param name="point"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool OccursAfter(
            string main, 
            string point,
            string query)
        {
            if (!main.Contains(point))
                return false;

            if (!main.Contains(query))
                return false;

            return main.IndexOf(query, main.IndexOf(point) + point.Length) != -1;
        }


        /// <summary> 
        /// Returns the position of the first alphanumeric or underline in a string. Returns -1 if none 
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        public static int PositionFirstAlphanumeric(
            string strSearch
            )
        {
            for (int i = 0; i < strSearch.Length; i++)
                if (StringTypeTestLib.IsAlphanumeric(strSearch.Substring(i, 1)))
                    return i;

            return -1;
        }



        /// <summary> Returns the psotion of the first non-alphanumeric or non-underline in a string. Returns -1 if none </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static int PositionFirstNonAlphanumericOrWhiteSpace(
            string search
            )
        {
            for (int i = 0; i < search.Length; i++)
                if (!StringTypeTestLib.IsAlphanumeric(search.Substring(i, 1)))
                    return i;

            return -1;
        }



        /// <summary> Returns the position of the last alphanumeric of non-underline in a string. Returns -1 if none </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static int PositionLastAlphanumeric(
            string search
            )
        {
            for (int i = 0; i < search.Length; i++)
                if (StringTypeTestLib.IsAlphanumeric(search.Substring(search.Length - 1 - i, 1)))
                    return search.Length - 1 - i;

            return -1;
        }



        /// <summary> 
        /// Returns the position of the last non-alphanumeric or non-underline in a string. Returns -1 if none 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static int PositionLastNonAlphanumericOrWhiteSpace(
            string search
            )
        {
            for (int i = 0; i < search.Length; i++)
                if (!StringTypeTestLib.IsAlphanumeric(search.Substring(search.Length - 1 - i, 1)))
                    return search.Length - 1 - i;

            return -1;
        }


        /// <summary>
        /// Removes all existing occurences of a substring from the end of a 
        /// string. Example, if the main string is "test", and the substring
        /// is "///", "test" is returned. If the substring is "//", "test/" is
        /// returned
        /// </summary>
        /// <param name="main"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveAllFromEnd(
            string main,
            string s
            )
        {
            while (main.EndsWith(s))
                main = main.Substring(0, main.Length - s.Length);

            return main;
        }


        /// <summary>
        /// Removes all existing occurences of a substring from the start of a 
        /// string. Example, if the main string is "///test", and the substring
        /// is "/", "test" is returned. If the substring is "//", "/test" is
        /// returned
        /// </summary>
        /// <param name="main"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveAllFromStart(
            string main,
            string s
            )
        {
            while (main.StartsWith(s))
                main = main.Substring(s.Length, main.Length - s.Length);

            return main;
        }


        /// <summary>
        /// Replaces the text between the start and end string, 
        /// for all occurrences of the start and end strings
        /// </summary>
        /// <param name="mainString"></param>
        /// <param name="subString"></param>
        /// <param name="startTag"></param>
        /// <param name="endTag"></param>
        /// <returns></returns>
        public static string ReplaceBeweenAll(
            string mainString,
            string subString,
            string startTag,
            string endTag
            )
        {

            int currentPosition = 0;
            int startPosition = 0;
            int endPosition = 0;

            StringBuilder x = new StringBuilder();

            // check input
            if (mainString.Length == 0 ||
                mainString.IndexOf(startTag) == -1 ||
                mainString.IndexOf(endTag) == -1)
            {
                return mainString;
            }

            while (true)
            {
                // gets positions of start and end tags
                startPosition = mainString.IndexOf(startTag, currentPosition);
                endPosition = mainString.IndexOf(endTag, currentPosition);

                // if either one of the tags aren't found, there is nothing more
                // to scan for - read up to the end of the sting and exit
                if (startPosition == -1 || endPosition == -1)
                {
                    x.Append(mainString.Substring(currentPosition, mainString.Length - currentPosition));
                    break;
                }


                // the the next end tags lies before the next start tag, there isnt
                // a proper start-stop wrapping of text to proceed. We must therefore
                // discount the end tag, and move on to the start tag and begin from
                // there. read everything up to that start tag, and then shift loop
                // along so we can look for the next end tag to match the start tag
                if (endPosition < startPosition)
                {
                    x.Append(mainString.Substring(currentPosition, startPosition - currentPosition));
                    currentPosition = startPosition;
                    continue;
                }

                // if the end tag lies somewhere within the start tag, there isn't 
                // anything that cab be replaced. we read up to the end tag, and
                // then shift loop so we can look for another start tag after the 
                // current end tag
                if (endPosition < startPosition + startTag.Length)
                {
                    x.Append(mainString.Substring(currentPosition, currentPosition + endTag.Length));
                    currentPosition = currentPosition + endTag.Length;
                    continue;
                }

                string test = mainString.Substring(currentPosition, startPosition - currentPosition) +
                    startTag +
                    subString +
                    endTag;

                x.Append(
                    mainString.Substring(currentPosition, startPosition - currentPosition) +
                    startTag +
                    subString +
                    endTag);

                currentPosition = endPosition + endTag.Length;

            }

            return x.ToString();
        }


        /// <summary> Replaces text between the first occurrence of a given string and the first occurence of a second given string </summary>
        /// <param name="main"></param>
        /// <param name="sub"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string ReplaceBetween(
            string main,
            string sub,
            string start,
            string end
            )
        {
            string firstPart = DivideString(main, start, 1, 1);;
            string secondPart = DivideString(main, end, 2, 2); ;
            string output = string.Empty;

            main = firstPart + sub + secondPart;
            output = main;

            return output;
        }

        /// <summary>
        /// Replaces everything between a start and end tage, including the start and end tag. First occurrence replace only.
        /// </summary>
        /// <param name="main"></param>
        /// <param name="sub"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string ReplaceBetweenInclusive(
            string main, 
            string sub,
            string start, 
            string end) 
        {
            string inner = ParserLib.ReturnBetween(main, start, end);
            string find = start + inner + end;
            return main.Replace(find, sub);
        }

        /// <summary> Finds and replaces text while ignore cases. </summary>
        /// <param name="main"></param>
        /// <param name="sub"></param>
        /// <param name="replaceWith"></param>
        /// <returns></returns>
        public static string ReplaceNoCase(
            string main,
            string sub,
            string replaceWith
            )
        {
            //THIS IS A CASE-UNSPECIFIC WAY OF REPLACING TEXT IN A STRING
            //METHOD IS PRETTY SLOPPY NOW, AND NEEDS TO BE TIGHTENED UP WITH FEWER VARS
            int i;
            int intOccurences;
            int intCurrentPosition;
            int instNextPosition;
            string strStringTemp;
            string strSubStrTemp;
            string strOutput = "";



            if (main.Length == 0)
            {//Exit Function;
            }

            //MAKES TEMPORARY COPIES IN LOWER CASE
            strStringTemp = main.ToLower();
            strSubStrTemp = sub.ToLower();

            //COUNTS HOW MANY TIMES THE SUBSTRING OCCURS IN STRING
            intOccurences = 0;
            intOccurences = StringCount(strStringTemp, strSubStrTemp);

            if (intOccurences != 0)
            {

                //GETS FIRST PART OF STRING, UP UNTIL FIRST OCCURENCE OF SUBSTRING
                strOutput = main.Substring(0, IndexOfFixed(strStringTemp, strSubStrTemp));

                //FIND POSITION IN STRING WHERE FIRST SUBSTR OCCURS
                intCurrentPosition = IndexOfFixed(strStringTemp, strSubStrTemp);


                for (i = 0; i < intOccurences; i++)
                {
                    //APPENDS REPLACEWITH STRING
                    strOutput += replaceWith;

                    //FINDS POSITION OF NEXT SUBSTR
                    instNextPosition = IndexOfFixed(strStringTemp, strSubStrTemp, intCurrentPosition + strSubStrTemp.Length);

                    if (instNextPosition == -1)
                    {
                        //IE, IF THIS IS THE LAST ARM OF THE LOOP AND THERE ARE NO MORE SUBSTRINGS LEFT AFTER THIS IN MAIN SRING
                        //APPENDS THE REMAINDER OF THE MAIN STRING
                        strOutput += main.Substring(intCurrentPosition + sub.Length, main.Length - intCurrentPosition - sub.Length);
                    }
                    else
                    {
                        //GETS ALL TEXT UP TO THE NEXT strReplaceWith OCCURENCE AND APPENDS TO OUTPUT STRING
                        strOutput = strOutput + main.Substring(intCurrentPosition + sub.Length, instNextPosition - intCurrentPosition - replaceWith.Length);
                    }

                    //FIND POSITION IN STRING WHERE SUBSTR OCCURS
                    intCurrentPosition = IndexOfFixed(strStringTemp, strSubStrTemp, intCurrentPosition + replaceWith.Length);


                }

            }
            else
            {
                //IF NO OCCURENCES OF SUBSTRING IN STRING, RETURNS UNMODIFIED STRING
                strOutput = main;
            }


            return strOutput;
        }


        /// <summary> Returns all text in a string after the first occurrence of a substring </summary>
        /// <param name="theString"></param>
        /// <param name="find"></param>
        /// <returns></returns>
        public static string ReturnAfter(
            string main,
            string sub
            )
        {
            int intReturnStringStartPosition;
            int intReturnStringLength;


            if ((main.Length == 0) || (sub.Length == 0) || (main.IndexOf(sub) == -1))
            {
                //error conditions here
                return "";
            }
            else
            {
                intReturnStringStartPosition = IndexOfFixed(main, sub) + sub.Length;
                intReturnStringLength = main.Length - intReturnStringStartPosition;
                return main.Substring(intReturnStringStartPosition, intReturnStringLength);
            }
        }



        /// <summary> Returns all text after the nth occurrence of a substring, when n is given </summary>
        /// <param name="main"></param>
        /// <param name="sub"></param>
        /// <param name="intTagCount"></param>
        /// <returns></returns>
        public static string ReturnAfterCount(
            string main,
            string sub,
            int intTagCount
            )
        {
            string output = string.Empty;
            int intTagPosition;


            //FINDS THE STRING POSITION OF THE TAG
            intTagPosition = IndexOfFixed(main, sub, 0, intTagCount);

            //CHECKS IF THERE IS ANY STRING AFTER THE TAG POSITION
            if (main.Length > (intTagPosition + sub.Length))
            {
                //RETURNS THE REQUIRED STRING
                output = main.Substring(intTagPosition + sub.Length, main.Length - intTagPosition - sub.Length);

            }

            return output;
        }



        /// <summary> Returns all in string after last occurrence of a substring</summary>
        /// <param name="main"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static string ReturnAfterLast(
            string main,
            string sub
            )
        {

            // if substring doesn't exist in main string, returns zero length string
            if (main.IndexOf(sub) == -1)
                return string.Empty;

            // if no text after substring, returns zero length string
            if (main.Length - 1 == main.LastIndexOf(sub))
                return string.Empty;

            // if reaches here, proceed to find desired substring
            return main.Substring(main.LastIndexOf(sub) + sub.Length, main.Length - main.LastIndexOf(sub) - sub.Length);
        }



        /// <summary> 
        /// Returns all text in a string from the first occurrence of a substring to the first occurrence 
        /// of another substring. 
        /// </summary>
        /// <param name="main"></param>
        /// <param name="startTag"></param>
        /// <param name="endTag"></param>
        /// <returns></returns>
        public static string ReturnBetween(
            string main,
            string startTag,
            string endTag
            )
        {
            //note the argument for the end_tag. start searching for it one after the start_tag incase tags are equal. that way, it won't detect one tag for both arguments.

            if (!main.Contains(startTag) || !main.Contains(endTag))
                return string.Empty;
            else
            {
                if (!main.Contains(startTag) || !main.Contains(endTag))
                    return string.Empty;
                else
                {
                    int startPosition = main.IndexOf(startTag) + startTag.Length;
                    if (startPosition >= main.Length)
                        return string.Empty;
                    int endPosition = main.IndexOf(endTag, startPosition);
                    if (endPosition >= main.Length)
                        return string.Empty;

                    return main.Substring(startPosition, endPosition - startPosition);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="main"></param>
        /// <param name="startTag"></param>
        /// <param name="endTag"></param>
        /// <returns></returns>
        public static string[] ReturnBetweenAll(
            string main,
            string startTag,
            string endTag
            )
        {
            List<string> items = new List<string>();


            while (true)
            {
                if (!main.Contains(startTag) || !main.Contains(endTag))
                    break;
                else
                {
                    int startPosition = main.IndexOf(startTag) + startTag.Length;
                    if (startPosition >= main.Length)
                        break;
                    int endPosition = main.IndexOf(endTag, startPosition);
                    if (endPosition >= main.Length)
                        break;

                    string find = main.Substring(startPosition, endPosition - startPosition);
                    items.Add(find);

                    if (endPosition + endTag.Length >= main.Length)
                        break;
                    main = main.Substring(endPosition + endTag.Length);
                }
            }

            return items.ToArray();

        }


        /// <summary>
        /// Gets all text occurring between the first occurrent of the starttag and the LAST occurrence of
        /// the end tag
        /// </summary>
        /// <param name="main"></param>
        /// <param name="startTag"></param>
        /// <param name="endTag"></param>
        /// <returns></returns>
        public static string ReturnBetweenLong(string main, string startTag, string endTag)
        {
            int startTagPosition;
            int endTagPosition;

            //note the argument for the end_tag. start searching for it one after the start_tag incase tags are equal. that way, it won't detect one tag for both arguments.

            if ((main.IndexOf(startTag) == -1) || (main.IndexOf(endTag, 1 + main.IndexOf(startTag)) == -1))
            {
                //ERROR CONDITION
                return "";
            }
            else
            {
                //----------------------------------------------------
                startTagPosition = main.IndexOf(startTag) + 1;
                endTagPosition = main.LastIndexOf(endTag);    //here too, incase the tags are identical, check endtag pos taking into account start_tag, so can't mistakenly detect starttag when scannign for endtag

                if (endTagPosition - startTagPosition + startTag.Length < 1)
                {
                    //ERROR CONDITION' if there's nothing between the two tags, leaves function
                    return "";
                }
                else
                {
                    main = main.Substring(startTagPosition + startTag.Length - 1, endTagPosition - startTagPosition - startTag.Length + 1);
                    return main.Trim();
                }
                //----------------------------------------------------

            }
        }

        /// <summary> 
        /// Returns all text in a string from between the occurrence of a substring and the occurrence 
        /// of a another substring. Allows for multiple occurrences of both substrings, and the specification of which
        /// respective occurrence will be used. 
        /// </summary>
        /// <param name="main"></param>
        /// <param name="startTag"></param>
        /// <param name="startTagCount"></param>
        /// <param name="endTag"></param>
        /// <param name="endTagCount"></param>
        /// <returns></returns>
        public static string ReturnBetweenCount(
            string main,
            string startTag,
            int startTagCount,
            string endTag,
            int endTagCount
            )
        {
            int position_start_tag;
            int position_end_tag;
            int start_tag_length;
            string result = "";


            if (!(startTagCount > 0) || !(endTagCount > 0))
                return string.Empty;

            if ((StringCount(main, startTag)) < (startTagCount))
                return string.Empty;

            if (StringCount(main, endTag) < endTagCount)
                return string.Empty;




            //note the argument for the end_tag. start searching for it one after the start_tag incase tags are equal. that way, it won't detect one tag for both arguments.
            if ((main.IndexOf(startTag) == -1))
            {
                //error conditions here
            }
            else if (IndexOfFixed(main, endTag, 0 + IndexOfFixed(main, startTag)) == -1)
            {
                //error conditions here
            }
            else
            {
                start_tag_length = startTag.Length;

                position_start_tag = IndexOfFixed(main, startTag, 0, startTagCount);

                position_end_tag = IndexOfFixed(main, endTag, 0, endTagCount);


                if (position_end_tag - position_start_tag + start_tag_length < 1)
                {
                    //Then Exit Function  ' if there's nothing between the two tags, leaves function
                }
                else
                {
                    result += main.Substring(position_start_tag + start_tag_length, position_end_tag - position_start_tag - start_tag_length);
                }

            }

            return result;
        }



        /// <summary> 
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="intMaxChars"></param>
        /// <returns></returns>
        public static string ReturnMaxChars(
            string input,
            int maxChars
            )
        {
            if (input.Length == 0)
                return "";
            if (input.Length <= maxChars)
                return input;

            if (input.Length > maxChars)
                return input.Substring(0, maxChars);

            return string.Empty;
        }



        /// <summary> 
        /// Returns all text in a string until the first occurence of a substring 
        /// </summary>
        /// <param name="main"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static string ReturnUpto(
            string main,
            string sub
            )
        {
            int position;

            if ((main.Length == 0) || (sub.Length == 0) || (main.IndexOf(sub) == -1))
                return string.Empty;


            position = IndexOfFixed(main, sub);
            main = main.Substring(0, position);
            return main;
        }

        /// <summary> Returns all text upto the nth occurrence of a substring, when n is given</summary>
        /// <param name="main"></param>
        /// <param name="sub"></param>
        /// <param name="intTagCount"></param>
        /// <returns></returns>
        public static string ReturnUptoCount(
            string main,
            string sub,
            int count
            )
        {
            string output = string.Empty;
            int intTagPosition;

            //FINDS THE STRING POSITION OF THE TAG
            intTagPosition = IndexOfFixed(main, sub, 0, count);

            //RETURNS THE REQUIRED STRING
            output = main.Substring(0, intTagPosition);

            return output;

        }



        /// <summary> 
        /// Returns all in string up to the last occurrence of a substring 
        /// </summary>
        /// <param name="main"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static string ReturnUptoLast(
            string main,
            string sub
            )
        {
            // if substring doesn't exist in main string, returns zero length string
            if (main.IndexOf(sub) == -1)
                return "";

            // if no text before substring, returns the main string
            if (main.Length == sub.Length)
                return "";

            // if reaches here, proceed to find desired substring
            return main.Substring(0, main.LastIndexOf(sub));
        }


        /// <summary>
        /// Safe substring that doesn't lose its shit if requested length exceeds available length.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubString(string s, int length)
        {
            if (s.Length < length)
                return s;
            return s.Substring(length);
        }

        /// <summary> 
        /// Counts how many times a substring occurs in a string 
        /// </summary>
        /// <param name="main"></param>
        /// <param name="stringsub"></param>
        /// <returns></returns>
        public static int StringCount(
            string main,
            string sub
            )
        {
            int i = 0;

            // simple "error" catching
            // exits function if either strMainString or strSubString are zero length, of if strSubString does not occur
            // in strMainString

            if (main.Length == 0)
                return 0;

            if (sub.Length == 0)
                return 0;

            if (main.IndexOf(sub) == -1)
                return 0;

            // loops through strMainString counting how many times strSubString occurs
            while (main.IndexOf(sub) != -1)
            {
                i++;
                main = main.Substring(IndexOfFixed(main, sub) + sub.Length, main.Length - sub.Length - IndexOfFixed(main, sub));
            }


            return i;
        }


        /// <summary>
        /// Returns the in-string position where two strings begin to differ from eachother.
        /// Returns -1 if strings are identical for the entire length of the shortest of the
        /// two. "dog" and "dog" with return -1. "dog" and "doghouse" with return -1; "dog"
        /// and "dag" will return 1. "dog" and "cat" will return 0.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int Trace(
            string first,
            string second
            )
        {
            int position = 0;

            while (position < first.Length && position < second.Length)
            {
                if (first.Substring(position, 1) != second.Substring(position, 1))
                    return position;

                position++;

            }

            return -1;

        }



        /// <summary>
        /// Returns the in-stirng position, from end of both strings, were the string begin to differ
        /// from eachother. Returns -1 if strings are identical for the entire length of the shortest
        /// of the two. "dog" and "hog" returns 0. 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int TraceFromEnd(
            string first,
            string second
            )
        {

            int position = 0;

            position = first.Length - 1;
            if (second.Length < first.Length)
                position = second.Length;

            while (position > -1)
            {
                if (first.Substring(position, 1) != second.Substring(position, 1))
                    return position;

                position--;

            }

            return -1;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveNonAlphanumeric(string s)
        {
            char[] arr = s.ToCharArray();

            arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c)
                                              || char.IsWhiteSpace(c)
                                              || c == '-')));
            s = new string(arr);

            return s;
        }

        /// <summary>
        /// Returns the index of the first occurrence of lookfor after the occurence of startstring.
        /// Returns -1 for no occurences
        /// </summary>
        /// <param name="main"></param>
        /// <param name="startstring"></param>
        /// <param name="lookfor"></param>
        /// <returns></returns>
        public static int IndexAfter(string main, string startstring, string lookfor)
        {
            if (main.Length == 0 || startstring.Length == 0 || lookfor.Length == 0)
                return -1;
            int startat = main.IndexOf(startstring) + startstring.Length;
            if (startat > main.Length)
                return -1;
            return main.IndexOf(lookfor, startat);
        }

        /// <summary>
        /// http://www.west-wind.com/weblog/posts/2009/Feb/05/Html-and-Uri-String-Encoding-without-SystemWeb
        /// HTML-encodes a string and returns the encoded string. Does NOT require a web context, hence its inclusion.
        /// </summary>
        /// <param name="text">The text string to encode. </param>
        /// <returns>The HTML-encoded text.</returns>
        public static string HtmlEncode(string text)
        {
            if (text == null)
                return null;

            StringBuilder sb = new StringBuilder(text.Length);

            int len = text.Length;
            for (int i = 0; i < len; i++)
            {
                switch (text[i])
                {

                    case '<':
                        sb.Append("&lt;");
                        break;
                    case '>':
                        sb.Append("&gt;");
                        break;
                    case '"':
                        sb.Append("&quot;");
                        break;
                    case '&':
                        sb.Append("&amp;");
                        break;
                    default:
                        if (text[i] > 159)
                        {
                            // decimal numeric entity
                            sb.Append("&#");
                            sb.Append(((int)text[i]).ToString(CultureInfo.InvariantCulture));
                            sb.Append(";");
                        }
                        else
                            sb.Append(text[i]);
                        break;
                }
            }
            return sb.ToString();
        }


    }
}

