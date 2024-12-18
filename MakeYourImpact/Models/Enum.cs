namespace MakeYourImpact.Models;

public enum ProjectStatus
{
    InReview,
    Verified,
    Canceled,
    InProgress,
    Completed
}

public enum UserRole
{
    Initiator,  // Людина, яка створює волонтерський проект.
    Volunteer,  // Волонтер, який долучається до проекту.
    Admin,      // Адміністратор системи.
    Donor       // Донор, який фінансово чи матеріально підтримує проект.
}
