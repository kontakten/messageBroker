using System;

public static class TimeStampHandler{
    public static bool RegulateEvenTimeStamp(DateTime ReceivedMessageTimestamp)
    {
        try
        {
            return ReceivedMessageTimestamp.TimeOfDay.Seconds % 2 == 0;
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex);
            return false;
        }
    }
}