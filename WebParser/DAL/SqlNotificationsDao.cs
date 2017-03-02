using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL
{
  public class SqlNotificationsDao
  {
    //NotificationsEntities _context = new NotificationsEntities();

    private static IProductDao _uniqueNotificationsDao;

    private SqlNotificationsDao()
    {
    }

    public List<Message> GetAllMessages()
    {
      using (var context = new NotificationsEntities())
      {
        return
          context.Messages.Select(
            q =>
              new Message(q.ID, q.Title, q.MessageText, q.TimeSent.Value, q.Link, q.Language, q.Application.Value,
                q.User.Value, q.MessageID)).ToList();
      }
    }

    public List<User> GetAllUsers()
    {
      using (var context = new NotificationsEntities())
      {
        return context.Users.Select(q => new User(q.ID, q.Username, q.Password)).ToList();
      }
    }

    public List<Application> GetAllApplications()
    {
      using (var context = new NotificationsEntities())
      {
        return context.Applications.Select(q => new Application(q.ID, q.Name, q.Languages.Split(',').ToList())).ToList();
      }
    }

    public void AddMessage(Message message)
    {
      using (var context = new NotificationsEntities())
      {
        var messageDb = (Messages) ConvertToStoredMessage(message);
        context.Messages.Add(messageDb);
        context.SaveChanges();
      }
    }

    public void AddUser(User user)
    {
      using (var context = new NotificationsEntities())
      {
        var userDb = (Users) ConvertToStoredUser(user);
        context.Users.Add(userDb);
        context.SaveChanges();
      }
    }

    public void AddApplication(Application application)
    {
      using (var context = new NotificationsEntities())
      {
        var applicationDb = (Applications) ConvertToStoredApplication(application);
        context.Applications.Add(applicationDb);
        context.SaveChanges();
      }
    }

    public User GetUserById(int id)
    {
      using (var context = new NotificationsEntities())
      {
        return ConvertToUser(context.Users.Find(id));
      }
    }

    public Application GetApplicationById(int id)
    {
      using (var context = new NotificationsEntities())
      {
        return ConvertToApplication(context.Applications.Find(id));
      }
    }

    public Message GetMessageById(int id)
    {
      using (var context = new NotificationsEntities())
      {
        return ConvertToMessage(context.Messages.Find(id));
      }
    }

    public void DeleteMessageById(int id)
    {
      using (var context = new NotificationsEntities())
      {
        context.Messages.Remove(context.Messages.Where(x => x.ID == id).Select(x => x).FirstOrDefault());
      }
    }

    public void DeleteUserById(int id)
    {
      using (var context = new NotificationsEntities())
      {
        context.Users.Remove(context.Users.Where(x => x.ID == id).Select(x => x).FirstOrDefault());
      }
    }

    public void DeleteApplicationById(int id)
    {
      using (var context = new NotificationsEntities())
      {
        context.Applications.Remove(context.Applications.Where(x => x.ID == id).Select(x => x).FirstOrDefault());
      }
    }

    public List<Message> GetAllUnsendedMessages()
    {
      using (var context = new NotificationsEntities())
      {
        var mess =
          context.Messages.Where(x => x.MessageID != null)
            .Select(ConvertToMessage)
            .OrderByDescending(n => n.TimeSent)
            .ToList();
        return mess;
      }
    }

    public void EditMessage(Message message)
    {
      using (var context = new NotificationsEntities())
      {
        var messageDb = context.Messages.First(n => n.ID == message.ID);
        messageDb.Title = message.Title;
        messageDb.MessageText = message.MessageText;
        messageDb.TimeSent = message.TimeSent;
        messageDb.Link = message.Link;
        messageDb.Language = message.Language;
        messageDb.Application = message.Application;
        messageDb.User = message.User;
        messageDb.MessageID = message.MessageID;
      }
    }

    public void EditApplication(Application application)
    {
      using (var context = new NotificationsEntities())
      {
        var appDb = context.Applications.First(n => n.ID == application.ID);
        appDb.Name = application.AppName;
        appDb.Languages = string.Join(",", application.LanguagesList);
      }
    }

    public void EditUser(User user)
    {
      using (var context = new NotificationsEntities())
      {
        var userDb = context.Users.First(n => n.ID == user.ID);
        userDb.Username = user.Username;
        userDb.Password = user.Password;
      }
    }

    public object ConvertToStoredMessage(Message message)
    {
      var result = new Messages
      {
        ID = message.ID,
        Title = message.Title,
        MessageText = message.MessageText,
        TimeSent = message.TimeSent,
        Link = message.Link,
        Language = message.Language,
        Application = message.Application,
        User = message.User,
        MessageID = message.MessageID
      };
      return result;
    }

    public object ConvertToStoredUser(User user)
    {
      var result = new Users
      {
        ID = user.ID,
        Username = user.Username,
        Password = user.Password
      };
      return result;
    }

    public object ConvertToStoredApplication(Application application)
    {
      var result = new Application
      {
        ID = application.ID,
        AppName = application.AppName,
        LanguagesList = application.LanguagesList
      };
      return result;
    }

    public Message ConvertToMessage(Messages message)
    {
      return new Message
      {
        ID = message.ID,
        Title = message.Title,
        MessageText = message.MessageText,
        TimeSent = message.TimeSent.Value,
        Link = message.Link,
        Language = message.Language,
        Application = message.Application.Value,
        User = message.User.Value,
        MessageID = message.MessageID
      };
    }

    public User ConvertToUser(Users user)
    {
      return new User
      {
        ID = user.ID,
        Username = user.Username,
        Password = user.Password
      };
    }

    public Application ConvertToApplication(Applications application)
    {
      return new Application
      {
        ID = application.ID,
        AppName = application.Name,
        LanguagesList = application.Languages.Split(',').ToList()
      };
    }

    public static IProductDao GetInstance()
    {
      return _uniqueNotificationsDao ?? (_uniqueNotificationsDao = new SqlNotificationsDao());
    }
  }
}