﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Library.Enums
{
    public static class Alias
    {
        public const string Left_ = "left_";
        public const string Left = "left";
        public const string Right_ = "right_";
        public const string Right = "right";
        public const string Source = Left;
        public const string Target = Right;
        public const string Primary_Key = "primary_key";
        public const string Debug = @".\debug\";
    }
    public enum Sources
    {
        Left,
        Right,
    }

    public enum ComparisonTypeEnum
    {
       None,
       String,
       Date,
       Datetime,
       Integer,
       Float,
       Boolean,
    }
    public enum OperatorEnums
    {
        Equals,
        Contains,
        EndsWidth,
        StartsWidth,
        In,        
        Table
    }
    
}
