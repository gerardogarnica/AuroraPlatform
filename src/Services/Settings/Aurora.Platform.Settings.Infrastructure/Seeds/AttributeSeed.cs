using Aurora.Framework.Repositories;
using Aurora.Platform.Settings.Domain.Entities;

namespace Aurora.Platform.Settings.Infrastructure.Seeds
{
    public class AttributeSeed : ISeedData<SettingsContext>
    {
        public void Seed(SettingsContext context)
        {
            var securityAttributes = context
                .AttributeSettings
                .Where(x => x.ScopeType.Equals("Security"))
                .ToList();

            if (!securityAttributes.Any(x => x.Code.Equals("UserPasswordExpirationPolicy")))
                context.AttributeSettings.Add(CreateUserPasswordExpirationPolicyAttribute());

            if (!securityAttributes.Any(x => x.Code.Equals("UserPasswordExpirationDays")))
                context.AttributeSettings.Add(CreateUserPasswordExpirationDaysAttribute());

            if (!securityAttributes.Any(x => x.Code.Equals("UserPasswordPatternPolicy")))
                context.AttributeSettings.Add(CreateUserPasswordPatternPolicyAttribute());

            if (!securityAttributes.Any(x => x.Code.Equals("UserPasswordHistoryValidationCount")))
                context.AttributeSettings.Add(CreateUserPasswordHistoryValidationCountAttribute());

            context.SaveChanges();
        }

        private static AttributeSetting CreateUserPasswordExpirationPolicyAttribute()
        {
            return new AttributeSetting()
            {
                Code = "UserPasswordExpirationPolicy",
                Name = "Enforce password expiration policy.",
                Description = "Indicates if the password expiration policy is enabled.",
                ScopeType = "Security",
                DataType = "Boolean",
                Configuration = "<booleanSetting><defaultValue>true</defaultValue></booleanSetting>",
                IsVisible = true,
                IsEditable = false,
                IsActive = true
            };
        }

        private static AttributeSetting CreateUserPasswordExpirationDaysAttribute()
        {
            return new AttributeSetting()
            {
                Code = "UserPasswordExpirationDays",
                Name = "Days of password expiration.",
                Description = "Number of days of password expiration since the last update.",
                ScopeType = "Security",
                DataType = "Integer",
                Configuration = "<integerSetting><minValue>1</minValue><maxValue>365</maxValue><defaultValue>365</defaultValue></integerSetting>",
                IsVisible = true,
                IsEditable = true,
                IsActive = true
            };
        }

        private static AttributeSetting CreateUserPasswordPatternPolicyAttribute()
        {
            return new AttributeSetting()
            {
                Code = "UserPasswordPatternPolicy",
                Name = "Password validation pattern.",
                Description = "Set the password validation pattern (if it is blank it does not apply).",
                ScopeType = "Security",
                DataType = "Text",
                Configuration = "<textSetting><minLength>0</minLength><maxLength>200</maxLength><defaultValue/><pattern/></textSetting>",
                IsVisible = true,
                IsEditable = true,
                IsActive = true
            };
        }

        private static AttributeSetting CreateUserPasswordHistoryValidationCountAttribute()
        {
            return new AttributeSetting()
            {
                Code = "UserPasswordHistoryValidationCount",
                Name = "Number of historical passwords to validate.",
                Description = "Indicates the number of last passwords to validate when a user updates the password.",
                ScopeType = "Security",
                DataType = "Integer",
                Configuration = "<integerSetting><minValue>1</minValue><maxValue>100</maxValue><defaultValue>1</defaultValue></integerSetting>",
                IsVisible = true,
                IsEditable = true,
                IsActive = true
            };
        }
    }
}