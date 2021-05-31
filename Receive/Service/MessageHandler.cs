using System;
using System.Linq;
public static class MessageHandler{
    
    public static void AddMessagesToTable(Message evenMsg)
    {
        try
        {

            using (var db = new MessageContext())
            {

                System.Console.WriteLine($"Inserting Message with ID: {evenMsg.ID}");
                
                
                var msg = db.Messages.SingleOrDefault(m => m.ID == evenMsg.ID);

                if(msg == null){
                    db.Messages.Add(evenMsg);
                }else{
                    msg.Timestamp = evenMsg.Timestamp;
                    db.Messages.Update(msg);
                }

                db.SaveChanges();
            }

        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex);
            throw;
        }
    }
}