using System;
using System.Collections.Generic;
using com.paralib.Data;

namespace com.paralib.DataAnnotations
{
    public class ParaTypes
    {
        private static readonly Lazy<ParaTypes> _instance = new Lazy<ParaTypes>(() => new ParaTypes());
        private Dictionary<string, ParaType> _paraTypes = new Dictionary<string, ParaType>();

        public const string Key = nameof(Key);
        public const string Blob = nameof(Blob);
        public const string DateTime = nameof(DateTime);
        public const string Time = nameof(Time);
        public const string Bool = nameof(Bool);
        public const string Decimal = nameof(Decimal);
        public const string Int32 = nameof(Int32);
        public const string Int64 = nameof(Int64);
        public const string Guid = nameof(Guid);

        public const string ParaString = nameof(ParaString);

        public const string Address = nameof(Address);
        public const string City = nameof(City);
        public const string State = nameof(State);
        public const string Zip = nameof(Zip);
        public const string Zip4 = nameof(Zip4);
        public const string Phone = nameof(Phone);
        public const string Name = nameof(Name);
        public const string Description = nameof(Description);
        public const string Comment = nameof(Comment);
        public const string Note = nameof(Note);
        public const string Text = nameof(Text);
        public const string LongText = nameof(LongText);
        public const string MaxText = nameof(MaxText);
        public const string Password = nameof(Password);
        public const string Currency = nameof(Currency);
        public const string Url = nameof(Url);
        public const string Email = nameof(Email);
        public const string Path = nameof(Path);

        private ParaTypes()
        {
            //basic types
            _paraTypes.Add(nameof(Key), new KeyType(nameof(Key)));
            _paraTypes.Add(nameof(Blob), new BlobType(nameof(Blob)));
            _paraTypes.Add(nameof(DateTime), new DateTimeType(nameof(DateTime)));
            _paraTypes.Add(nameof(Time), new TimeType(nameof(Time)));
            _paraTypes.Add(nameof(Bool), new BoolType(nameof(Bool)));
            _paraTypes.Add(nameof(Decimal), new DecimalType(nameof(Decimal)));
            _paraTypes.Add(nameof(Int32), new Int32Type(nameof(Int32)));
            _paraTypes.Add(nameof(Int64), new Int64Type(nameof(Int64)));
            _paraTypes.Add(nameof(Guid), new GuidType(nameof(Guid)));

            //pseudo types
            _paraTypes.Add(nameof(ParaString), null);


            //string types
            _paraTypes.Add(nameof(Address), new StringType(nameof(Address), 256));
            _paraTypes.Add(nameof(City), new StringType(nameof(City), 128));
            _paraTypes.Add(nameof(State), new StringType(nameof(State), 2));
            _paraTypes.Add(nameof(Zip), new StringType(nameof(Zip), 5) { RegEx = @"^[0-9]{5}$", BadFormatErrorMessage = "'{0}' must be a valid ZIP code", TooLongErrorMessage = "'{0}' must be a valid ZIP code" });
            _paraTypes.Add(nameof(Zip4), new StringType(nameof(Zip4), 10) { RegEx = @"^[0-9]{5}(-[0-9]{4})?$", BadFormatErrorMessage = "'{0}' must be a valid ZIP code", TooLongErrorMessage = "'{0}' must be a valid ZIP code" });
            _paraTypes.Add(nameof(Phone), new StringType(nameof(Phone), 13));
            _paraTypes.Add(nameof(Name), new StringType(nameof(Name), 64));
            _paraTypes.Add(nameof(Description), new StringType(nameof(Description), 128));
            _paraTypes.Add(nameof(Comment), new StringType(nameof(Comment), 256));
            _paraTypes.Add(nameof(Note), new StringType(nameof(Note), 512));
            _paraTypes.Add(nameof(Text), new StringType(nameof(Text), 1024));
            _paraTypes.Add(nameof(LongText), new StringType(nameof(LongText), 2048));
            _paraTypes.Add(nameof(MaxText), new StringType(nameof(MaxText), int.MaxValue));
            _paraTypes.Add(nameof(Password), new StringType(nameof(Password), 128));
            _paraTypes.Add(nameof(Currency), new StringType(nameof(Currency), 10) { RegEx = @"^\$?(\d{1,3},?(\d{3},?)*\d{3}(.\d{0,3})?|\d{1,3}(.\d{2})?)$", BadFormatErrorMessage = "'{0}' must be a currency value.", TooLongErrorMessage = "'{0}' is too large." });
            _paraTypes.Add(nameof(Url), new StringType(nameof(Url), 2000));

            /*
            Email:
                more complicated and supposedly 99%
                @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?" }
            */
            _paraTypes.Add(nameof(Email), new StringType(nameof(Email), 254) { RegEx = @".+\@.+\..+" });


            /*
            Path:
                The Win32 API imposed a limit of 260 characters, known as MAX_PATH.
                Windows NT does not support full pathnames longer than 32,767 bytes for NTFS. 
                Unicode versions of the Win32 API can use the NTFS length.
                Windows 10 allows for "opt-in" to use the NTFS length.
                Linux has a pathname limit of 4,096. 

                For cross-platform purposes, we use the Win32 length.

            */
            _paraTypes.Add(nameof(Path), new StringType(nameof(Path), 260));


        }

        public ParaType this[string name]
        {
            get
            {
                if (!_paraTypes.ContainsKey(name))
                {
                    throw new ParalibException($"ParaType \"{name}\" does not exist.");
                }

                return _paraTypes[name];
            }

        }


        internal static ParaTypes Instance
        {
            get
            {
                return _instance.Value;
            }
        }



    }

}
