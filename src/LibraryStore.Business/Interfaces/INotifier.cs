using LibraryStore.Business.Notifications;

namespace LibraryStore.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();

        List<Notification> GetNotifications();

        void Handle(Notification notification);
    }
}
