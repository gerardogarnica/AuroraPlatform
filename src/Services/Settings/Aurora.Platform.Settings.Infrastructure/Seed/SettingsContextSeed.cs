using Aurora.Platform.Settings.Domain.Entities;

namespace Aurora.Platform.Settings.Infrastructure.Seed
{
    public class SettingsContextSeed
    {
        public static async Task SeedAsync(SettingsContext context)
        {
            if (!context.Options.Any() && !context.AttributeSettings.Any())
            {
                context.Options.AddRange(GetGlobalOptionsList());
                context.AttributeSettings.AddRange(GetSecurityAttributes());

                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<OptionsList> GetGlobalOptionsList()
        {
            return new List<OptionsList>
            {
                new OptionsList()
                {
                    Code = "AttributeType",
                    Name = "Attribute Types",
                    Description = "Level or scope of attribute settings.",
                    IsVisible = true,
                    IsEditable = false,
                    Items = new List<OptionsListItem>
                    {
                        new OptionsListItem()
                        {
                            Code = "Security",
                            Description = "Global security attributes.",
                            IsEditable = false,
                            IsActive = true,
                            CreatedBy = "BATCH-USR",
                            CreatedDate = DateTime.UtcNow,
                            LastUpdatedBy = "BATCH-USR",
                            LastUpdatedDate = DateTime.UtcNow
                        }
                    }
                }
            };
        }

        private static IEnumerable<AttributeSetting> GetSecurityAttributes()
        {
            return new List<AttributeSetting>
            {
                // UserPasswordExpirationPolicy
                new AttributeSetting()
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
                },
                // UserPasswordExpirationDays
                new AttributeSetting()
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
                },
                // UserPasswordPatternPolicy
                new AttributeSetting()
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
                },
                // UserPasswordHistoryValidationCount
                new AttributeSetting()
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
                }
            };
        }
    }
}