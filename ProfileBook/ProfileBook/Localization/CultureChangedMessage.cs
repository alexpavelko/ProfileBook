using System.Globalization;

namespace ProfileBook.Localization
{
    public class CultureChangedMessage
    {
        public CultureInfo NewCultureInfo { get; }

        public CultureChangedMessage(string lngName) 
            : this (new CultureInfo(lngName))
        { }

        public CultureChangedMessage(CultureInfo newCultureInfo)
        {
            NewCultureInfo = newCultureInfo;
        }
    }
}
