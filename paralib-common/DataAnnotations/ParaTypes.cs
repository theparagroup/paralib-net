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
        public const string Time = nameof(Time);
        public const string Bool = nameof(Bool);

        public const string Email= nameof(Email);
        public const string Url = nameof(Url);
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
        public const string MaxText = nameof(MaxText);
        public const string Password = nameof(Password);
        public const string DateTime = nameof(DateTime);
        public const string Decimal = nameof(Decimal);
        public const string Int32 = nameof(Int32);

        private ParaTypes()
        {
            _paraTypes.Add(nameof(Key), new KeyType(nameof(Key)));
            _paraTypes.Add(nameof(Blob), new BlobType(nameof(Blob)));
            _paraTypes.Add(nameof(DateTime), new DateTimeType(nameof(DateTime)));
            _paraTypes.Add(nameof(Decimal), new DecimalType(nameof(Decimal)));
            _paraTypes.Add(nameof(Time), new TimeType(nameof(Time)));
            _paraTypes.Add(nameof(Bool), new BoolType(nameof(Bool)));
            _paraTypes.Add(nameof(Int32), new Int32Type(nameof(Int32))); 


            //more complicated and supposedly 99%
            //@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?" }
            _paraTypes.Add(nameof(Email), new StringType(nameof(Email)) { MaximumLength = 254, RegEx = @".+\@.+\..+" });

            _paraTypes.Add(nameof(Url), new StringType(nameof(Url)) { MaximumLength = 2000});
            _paraTypes.Add(nameof(Address), new StringType(nameof(Address)) { MaximumLength = 256});
            _paraTypes.Add(nameof(City), new StringType(nameof(City)) { MaximumLength = 128});
            _paraTypes.Add(nameof(State), new StringType(nameof(State)) { MaximumLength = 2});
            _paraTypes.Add(nameof(Zip), new StringType(nameof(Zip)) { MaximumLength = 5, RegEx= @"^[0-9]{5}$", BadFormatErrorMessage="'{0}' must be a valid ZIP code", TooLongErrorMessage = "'{0}' must be a valid ZIP code" });
            _paraTypes.Add(nameof(Zip4), new StringType(nameof(Zip4)) { MaximumLength = 10, RegEx=@"^[0-9]{5}(-[0-9]{4})?$", BadFormatErrorMessage = "'{0}' must be a valid ZIP code", TooLongErrorMessage = "'{0}' must be a valid ZIP code" });
            _paraTypes.Add(nameof(Phone), new StringType(nameof(Phone)) { MaximumLength = 13 });
            _paraTypes.Add(nameof(Name), new StringType(nameof(Name)) { MaximumLength = 64});
            _paraTypes.Add(nameof(Description), new StringType(nameof(Description)) { MaximumLength = 128});
            _paraTypes.Add(nameof(Comment), new StringType(nameof(Comment)) { MaximumLength = 256 });
            _paraTypes.Add(nameof(Note), new StringType(nameof(Note)) { MaximumLength = 512 });
            _paraTypes.Add(nameof(Text), new StringType(nameof(Text)) { MaximumLength = 1024});
            _paraTypes.Add(nameof(MaxText), new StringType(nameof(MaxText)) { MaximumLength = int.MaxValue});
            _paraTypes.Add(nameof(Password), new StringType(nameof(Password)) { MaximumLength = 128 });
                        
          
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
