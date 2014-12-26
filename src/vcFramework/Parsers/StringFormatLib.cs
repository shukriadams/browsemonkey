//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

namespace vcFramework.Parsers
{
    /// <summary> Static collection of string manipulation methods 
    /// primarily used for formatting.</summary>
    public class StringFormatLib
    {

        /// <summary> Mimics behaviour of ASP url encode method.  
        /// </summary>
        public static string UrlEncode(
            string strSource
            )
        {

            string strOutput = "";


            strOutput = strSource;
            //REPLACES SPACES
            strOutput = strOutput.Replace(" ", "+");

            return strOutput;
        }



        /// <summary> Indents all lines in a string block with the 
        /// specified indenter. Can be used to format "reply" 
        /// messages in emails, as well as forcing a fixed linewidth 
        /// onto a string block. </summary>
        /// <param name="strText"></param>
        /// <param name="strIndentCharacter"></param>
        /// <param name="intLineLength"></param>
        /// <returns></returns>
        public static string IndentText(
            string strText,
            string strIndentCharacter,
            int intLineLength
            )
        {
            //INDENTS A BODY OF TEXT AS IN REPLY EMAILS : TEXT IS INDENTED WITH A CHARACTER, AND REARRANGED ACCORDINGLY.

            int intLastSpacePosition;
            int intLastLineBreakPosition;
            string strMessageOrig;
            string strLineStretch = "";
            string strMessage = "";



            strMessageOrig = strText;

            while (strMessageOrig.Length > 0)
            {

                //CHECKS OF THE REMAINING strMessageOrig IS TOO SHORT TO PROCEED. IF SO, ADDS IT TO OUTPUT AND EXITS LOOP
                if (strMessageOrig.Length < intLineLength)
                {
                    //CHECKS IF THERE IS A LINEBREAK IN THE REMAIN LENGTH OF strMessageOrig
                    if (ParserLib.IndexOfFixed(strMessageOrig, "\r\n") != -1)
                    {
                        //INDENTS ALL REMAIN LINEBREAKS UNTIL NO strMessageOrig REMAINING
                        while (ParserLib.StringCount(strMessageOrig, "\r\n") > 0)
                        {
                            strMessage = strMessage + strIndentCharacter + strMessageOrig.Substring(0, ParserLib.IndexOfFixed(strMessageOrig, "\r\n")) + "\r\n";
                            strMessageOrig = strMessageOrig.Substring(ParserLib.IndexOfFixed(strMessageOrig, "\r\n") + 2, strMessageOrig.Length - ParserLib.IndexOfFixed(strMessageOrig, "\r\n") - 2);
                        }

                    }
                    else
                    {
                        //IF NO LINE BREAK, SIMPLY DUMPS REMAINING STRING INTO THE OUTPUT
                        strMessage = strMessage + strIndentCharacter + strMessageOrig;
                        strMessageOrig = "";
                    }
                }
                //IF strMessageOrig LENGTH IS GREATER THATN intLineLength
                else
                {
                    //IF LINELENGTH STRETCH HAS A LINEBREAK IN IT
                    if (ParserLib.IndexOfFixed(strMessageOrig.Substring(0, intLineLength), "\r\n") != -1)
                    //if (strMessageOrig.Substring(0, intLineLength).IndexOf("\r\n", 0) != -1)
                    {
                        //MAKES TEMP COPY OF LINELENTH PIECE, AND REMOVES LINELENGTH PIECE FROM strMessageOrig
                        intLastLineBreakPosition = strMessageOrig.Substring(0, intLineLength).LastIndexOf("\r\n");
                        strLineStretch = strMessageOrig.Substring(0, intLastLineBreakPosition + 2);
                        strMessageOrig = strMessageOrig.Substring(intLastLineBreakPosition + 2, strMessageOrig.Length - intLastLineBreakPosition - 2);

                        //INDENTS ALL REMAIN LINEBREAKS UNTIL NO strMessageOrig REMAINING
                        while (ParserLib.StringCount(strLineStretch, "\r\n") > 0)
                        {
                            strMessage = strMessage + strIndentCharacter + strLineStretch.Substring(0, ParserLib.IndexOfFixed(strLineStretch, "\r\n")) + "\r\n";
                            strLineStretch = strLineStretch.Substring(ParserLib.IndexOfFixed(strLineStretch, "\r\n") + 2, strLineStretch.Length - ParserLib.IndexOfFixed(strLineStretch, "\r\n") - 2);
                        }


                    }
                    //IF LINELENGHT STRETCH HAS A SPACE IN IT
                    else if (ParserLib.IndexOfFixed(strMessageOrig.Substring(0, intLineLength), " ") != -1)
                    //else if (strMessageOrig.Substring(0, intLineLength).IndexOf(" ", 0) != -1)
                    {
                        //IF THERE IS A SPACE SOMEWHERE IN THE LINE TO BE CUT, FINDS IT
                        intLastSpacePosition = strMessageOrig.Substring(0, intLineLength).LastIndexOf(" ");
                        strMessage += strIndentCharacter + strMessageOrig.Substring(0, intLastSpacePosition) + "\r\n";
                        strMessageOrig = strMessageOrig.Substring(intLastSpacePosition + 1, strMessageOrig.Length - intLastSpacePosition - 1);		//THE +1 -1 HAS BEEN ADDED TO FACTOR FOR THE " ", WHICH ENDS UP SLIPPING THROUGH ERRONEOUSLY
                    }
                    //IF THERE IS NO SPACE OR NO LINEBREAK TO CUT ON, BREAKS THE STRING AT THE STRETCH LENGTH POINT
                    else
                    {
                        //IF NO SPACE, CUT AT MAX LENGTH, AND ADD -
                        strMessage += strIndentCharacter + strMessageOrig.Substring(0, intLineLength - 1) + "-" + "\r\n";
                        strMessageOrig = strMessageOrig.Substring(intLineLength - 1, strMessageOrig.Length - intLineLength + 1);
                    }
                }
            }

            return strMessage;
        }




        /// <summary> 
        /// Adds the given padding text to the given main text, until main text reaches the given
        /// length. this can be used, for example, to add trailing white space to text to make it
        /// fit a given length
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="intTextMaxLength"></param>
        /// <returns></returns>
        public static string PadText(
            string strText,
            string strPadder,
            int intTextMaxLength
            )
        {
            int intOriginalStrTextLength = strText.Length;

            // returns unchanged strText if length is wrong
            if (strText.Length >= intTextMaxLength)
                return strText;

            // adds spaces 
            for (int i = 0; i < intTextMaxLength - intOriginalStrTextLength; i++)
                strText += strPadder;

            return strText;
        }



        /// <summary> 
        /// Produces a line mae from the strLineBlock text for a given length  </summary>
        /// <param name="strLineBlock"></param>
        /// <param name="intLineLength"></param>
        /// <returns></returns>
        public static string CharLine(
            string strLineBlock,
            int intLineLength
            )
        {
            string strOutput = "";

            // handles incorrect input
            if (strLineBlock.Length == 0 || intLineLength < 1)
                return "";

            for (int i = 0; i < intLineLength / strLineBlock.Length; i++)
                strOutput += strLineBlock;

            return strOutput;

        }



        /// <summary> Removes linebreaks from a string, but will 
        /// not remove paragraph breaks (double linebreaks)</summary>
        /// <param name="strSourceText"></param>
        /// <returns></returns>
        public static string RemoveLineBreaks(
            string strSourceText
            )
        {
            string strVCTemporaryLinebreak = ">vcTemp--LineBreakT--hingie0192---983<";

            // 1 - CONVERT PARAGRAPH BREAKS INTO SPECIAL PARAGRAPH BREAK
            strSourceText = strSourceText.Replace("\r\n\r\n", strVCTemporaryLinebreak);

            //2 - REMOVE ALL REMANING LINEBREAKS
            strSourceText = strSourceText.Replace("\r\n", "");

            //3 - CONVERT ALL SPECIAL PARAGRAPH BREAK INTO NORMAL PARAGRAPH BREAKS
            strSourceText = strSourceText.Replace(strVCTemporaryLinebreak, "\r\n\r\n");

            return strSourceText;
        }



        /// <summary> Converts all non-HTML formatted URLs in a string 
        /// into HTML-formatted URLS, and returns entire string. </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static string UrlConvert(
            string strInput
            )
        {
            string strOutput = "";
            string strURLText = "";
            string strCurrentURLStartTag = "";
            int intURLStartPosition;
            int intURLEndPosition;


            //1. STANDARDIZES TEXT
            strInput = ParserLib.ReplaceNoCase(strInput, "http://", "http://");
            strInput = ParserLib.ReplaceNoCase(strInput, "www.", "www.");

            while (strInput.Length > 0)
            {
                //DETERMINES START TAG OF FIRST URL - NOTE : THIS FEATURE ALLOWS FOR BOTH FULL AND PARTIAL URL START STRINGS ("http://" & "www.") TO BE SUPPORTED
                //FIRST, ALLOCATE DEFAULT VALUE
                strCurrentURLStartTag = "http://";       //SETS DEFAULT. IF NO DEFAULT SET, AND NO START TAG OCCURS IN STRING, A BLANK LINE WILL BEHAVE LIKE A START TAG
                if (strInput.IndexOf("http://") != -1)
                {
                    strCurrentURLStartTag = "http://";
                }
                else if (strInput.IndexOf("www.") != -1)
                {
                    strCurrentURLStartTag = "www.";
                }

                if ((strInput.IndexOf("http://") < strInput.IndexOf("www.")) && (strInput.IndexOf("http://") != -1))
                { strCurrentURLStartTag = "http://"; }

                if ((strInput.IndexOf("www://") < strInput.IndexOf("http://")) && (strInput.IndexOf("www://") != -1))
                { strCurrentURLStartTag = "www://"; }

                intURLStartPosition = strInput.IndexOf(strCurrentURLStartTag);       //FINDS WHERE URL STARTS;
                intURLEndPosition = strInput.IndexOf(" ", intURLStartPosition + 1);   //FINDS WHERE URL ENDS - A SPACE;

                if (intURLEndPosition == -1)
                { intURLEndPosition = strInput.Length; }    //IF CANNOT FIND AN END OF URL, MAKES TEH END OF THE WHOLE STRING THE END OF THE URL;

                if (intURLStartPosition != -1)
                {
                    //ADDS PRE-URL STIRNG TO OUTPUT;
                    strOutput = strOutput + strInput.Substring(0, intURLStartPosition);

                    strURLText = strInput.Substring(intURLStartPosition, intURLEndPosition - intURLStartPosition);

                    if (strCurrentURLStartTag == "www.")
                    {
                        //THIS IS REQUIRED TO TURN A PARTIAL URL START INTO A FULL ONE, BUT ONLY IN THE BEHIND-SIDE HTML CODE;
                        strURLText = "<a href='http:// '" + strURLText + "'>" + strURLText + "</a>";
                    }
                    else
                    {
                        //THIS IS THE NORMAL MODE;
                        strURLText = "<a href='" + strURLText + "'>" + strURLText + "</a>";
                    }

                    strOutput = strOutput + strURLText;
                    strInput = strInput.Substring(intURLEndPosition, strInput.Length - intURLEndPosition);
                }
                else
                {
                    //IF REACH HERE, NO URL STARTER FOUND IN strInput. DUMPS strInput INTO OUTPUT, SETS strInput TO "" AND THUS EXITS LOOP;
                    strOutput = strOutput + strInput;
                    strInput = "";
                }
            }

            return strOutput;

        }



        /// <summary> Truncates a string if it is longer than a 
        /// specified string, and adds "tail" text to it </summary>
        /// <param name="strTheString"></param>
        /// <param name="intMaxLength"></param>
        /// <param name="strEndString"></param>
        /// <returns></returns>
        public static string LimitStringLength(
            string strTheString,
            int intMaxLength,
            string strEndString
            )
        {
            if (strTheString.Length > intMaxLength)
                return (strTheString.Substring(intMaxLength - strEndString.Length) + strEndString);
            return "";
        }

    }
}
