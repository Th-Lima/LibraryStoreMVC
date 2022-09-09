using FluentValidation;
using FluentValidation.Results;
using LibraryStore.Business.Interfaces;
using LibraryStore.Models;

namespace LibraryStore.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notification(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notification(error.ErrorMessage);
            }
        }

        protected void Notification(string errorMessage)
        {
            _notifier.Handle(new Notifications.Notification(errorMessage));
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validatior = validation.Validate(entity);

            if (validatior.IsValid)
                return true;

            Notification(validatior);

            return false;
        }

    }
}
