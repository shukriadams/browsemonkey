//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;

namespace vcFramework.IO
{
    public class MappingException : Exception
    {
        readonly string _file;

        public string File
        {
            get 
            {
                return _file;
            }
        }

        public MappingException(string file)
        {
            _file = file;
        }
    }
}
