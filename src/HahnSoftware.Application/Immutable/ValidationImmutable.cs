namespace HahnSoftware.Immutable;

public class ValidationImmutable
{
    public static string Required(string field)
    {
        // Alternative: $"Please provide your {field.ToLower()}.";
        // Alternative: $"{field} is required to proceed."
        return $"{field} is required!";
    }
    
    public static string MinimumLength(string field, int length)
    {
        // Alternative: $"{field} should be at least {length} characters.";
        // Alternative: $"For security, please make your {field.ToLower()} at least {length} characters long."
        return $"{field} must be at least {length} characters long!";
    }
    
    public static string MaximumLength(string field, int length)
    {
        // Alternative: $"{field} cannot exceed {length} characters.";
        // Alternative: $"For security reasons, {field.ToLower()} must be shorter than {length} characters."
        return $"{field} must not exceed {length} characters long!";
    }

    public static string InvalidFormat(string field)
    {
        // Alternative: $"The {field.ToLower()} format is invalid. Please check and try again.";
        // Alternative: $"Please enter a valid {field.ToLower()} format."
        return $"Invalid {field} format!";
    }
    
    public static string Exists(string field)
    {
        // Alternative: $"This {field.ToLower()} is already registered. Please try a different one.";
        // Alternative: $"{field} is already in use. Did you mean to log in instead?"
        return $"{field} Already Exists!";
    }

    /*
    public static string PasswordsDontMatch() =>
        "The passwords you entered don't match. Please try again.";

    public static string InvalidCredentials() =>
        "The email or password you entered is incorrect. Please try again.";

    public static string AccountLocked() =>
        "Your account has been temporarily locked due to multiple failed attempts. Please try again later or reset your password.";

    public static string PasswordStrength() =>
        "Password must contain at least one uppercase letter, one number, and one special character.";
    */

    /*
    RuleFor(x => x.Password)
        .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
        .Matches(@"\d").WithMessage("Password must contain at least one number")
        .Matches(@"[\W]").WithMessage("Password must contain at least one special character");
    */
}
