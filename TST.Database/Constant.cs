namespace TST.Database
{
    public class Constant
    {
        public class Database
        {
            public const string CONNECTION_STRING = @"Server=DESKTOP-ECG76DG\SQLEXPRESS;Database=TstRecordings;Integrated Security=True;";

            //Track
            public const int TRACK_NAME_MAX_LENGTH = 50;
            public const int TRACK_DESCRIPTION_MAX_LENGHT = 500;
            public const int TRACK_DATA_MAX_LENGHT = 1000000000;
        }
    }
}
