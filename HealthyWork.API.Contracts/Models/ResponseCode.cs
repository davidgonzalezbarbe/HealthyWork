using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace HealthyWork.API.Contracts.Models
{
    public enum ResponseCode
    {
        #region Object Success Codes (200-249)
        #region Create
        [Description("Configuration was created successfully")]
        Configuration_Created = 210,
        [Description("Room was created successfully")]
        Room_Created = 211,
        [Description("User was created successfully")]
        User_Created = 212,
        [Description("value was created successfully")]
        Value_Created = 213,
        [Description("HeadQuarters was created successfully")]
        HQ_Created = 214,
        [Description("Telegram Push was created successfully")]
        TelegramPush_Created = 215,
        #endregion
        #region Read
        [Description("Configuration was found successfully")]
        Configuration_Found = 220,
        [Description("Room was found successfully")]
        Room_Found = 221,
        [Description("Value was found successfully")]
        Value_Found = 222,
        [Description("User was found successfully")]
        User_Found = 223,
        [Description("HeadQuarters was found successfully")]
        HQ_Found = 224,
        [Description("Telegram Push was found successfully")]
        TelegramPush_Found = 225,
        #endregion
        #region Update
        [Description("Configuration was updated successfully")]
        Configuration_Updated = 230,
        [Description("Room was updated successfully")]
        Room_Updated = 231,
        [Description("value was updated successfully")]
        Value_Updated = 232,
        [Description("User was updated successfully")]
        User_Updated = 233,
        [Description("HeadQuarters was updated successfully")]
        HQ_Updated = 234,
        [Description("Telegram Push was updated successfully")]
        TelegramPush_Updated = 235,
        #endregion
        #region Delete
        [Description("Configuration was deleted successfully")]
        Configuration_Deleted = 240,
        [Description("Room was deleted successfully")]
        Room_Deleted = 241,
        [Description("Value was deleted successfully")]
        Value_Deleted = 242,
        [Description("User was deleted successfully")]
        User_Deleted = 243,
        [Description("HeadQuarters was deleted successfully")]
        HQ_Deleted = 244,
        [Description("Telegram Push was deleted successfully")]
        TelegramPush_Deleted = 245,
        #endregion
        #region Others
        [Description("Object encrypted successfully")]
        Object_Encrypted = 246,
        [Description("Object desencrypted successfully")]
        Object_Desencrypted = 247,
        #endregion
        #endregion
        #region List Success Codes (250-299)
        #region Create
        [Description("Configurations was created successfully")]
        Configurations_Created = 250,
        [Description("Rooms was created successfully")]
        Rooms_Created = 251,
        [Description("Users was created successfully")]
        Users_Created = 252,
        [Description("values was created successfully")]
        Values_Created = 253,
        [Description("HeadQuarters was created successfully")]
        HQs_Created = 254,
        [Description("Telegram Pushes was created successfully")]
        TelegramPushes_Created = 255,
        #endregion
        #region Read
        [Description("Configurations was found successfully")]
        Configurations_Found = 260,
        [Description("Rooms was found successfully")]
        Rooms_Found = 261,
        [Description("Values were found successfully")]
        Values_Found = 262,
        [Description("Users was found successfully")]
        Users_Found = 263,
        [Description("HeadQuarters was found successfully")]
        HQs_Found = 264,
        [Description("Telegram Pushes was found successfully")]
        TelegramPushes_Found = 265,
        #endregion
        #region Update
        [Description("Configurations was updated successfully")]
        Configuratiosn_Updated = 270,
        [Description("Rooms was updated successfully")]
        Rooms_Updated = 271,
        [Description("Values was updated successfully")]
        Values_Updated = 272,
        [Description("Users was updated successfully")]
        Users_Updated = 273,
        [Description("HeadQuarters was updated successfully")]
        HQs_Updated = 274,
        [Description("Telegram Pushes was updated successfully")]
        TelegramPushes_Updated = 275,
        #endregion
        #region Delete
        [Description("Configurations was deleted successfully")]
        Configurations_Deleted = 280,
        [Description("Rooms was deleted successfully")]
        Rooms_Deleted = 281,
        [Description("Values was deleted successfully")]
        Values_Deleted = 282,
        [Description("Users was deleted successfully")]
        Users_Deleted = 283,
        [Description("HeadQuarters was deleted successfully")]
        HQs_Deleted = 284,
        [Description("Telegram Pushes was deleted successfully")]
        TelegramPushes_Deleted = 285,
        #endregion
        #region Others
        #endregion
        #endregion
        #region Object Error Codes(300-349)
        #region Create
        [Description("Configuration could not be created")]
        Configuration_NotCreated = 310,
        [Description("Room could not be created")]
        Room_NotCreated = 311,
        [Description("User could not be created")]
        User_NotCreated = 312,
        [Description("Value could not be created")]
        Value_NotCreated = 313,
        [Description("HeadQuarters could not be created")]
        HQ_NotCreated = 314,
        [Description("Telegram Push could not be created")]
        TelegramPush_NotCreated = 315,
        #endregion
        #region Read
        [Description("Configuration could not be found")]
        Configuration_NotFound = 320,
        [Description("Room could not be found")]
        Room_NotFound = 321,
        [Description("Value could not be found")]
        Value_NotFound = 322,
        [Description("User could not be found")]
        User_NotFound = 323,
        [Description("HeadQuarters could not be found")]
        HQ_NotFound = 324,
        [Description("Telegram Push could not be found")]
        TelegramPush_NotFound = 325,
        #endregion
        #region Update
        [Description("Configuration could not be updated")]
        Configuration_NotUpdated = 330,
        [Description("Room could not be updated")]
        Room_NotUpdated = 331,
        [Description("Value could not be updated")]
        Value_NotUpdated = 332,
        [Description("User could not be updated")]
        User_NotUpdated = 333,
        [Description("HeadQuarters could not be updated")]
        HQ_NotUpdated = 334,
        [Description("Telegram Push could not be updated")]
        TelegramPush_NotUpdated = 335,
        #endregion
        #region Delete
        [Description("Configuration could not be deleted")]
        Configuration_NotDeleted = 340,
        [Description("Room could not be deleted")]
        Room_NotDeleted = 341,
        [Description("value could not be deleted")]
        Value_NotDeleted = 342,
        [Description("User could not be deleted")]
        User_NotDeleted = 343,
        [Description("HeadQuarters could not be deleted")]
        HQ_NotDeleted = 344,
        [Description("Telegram Push could not be deleted")]
        TelegramPush_NotDeleted = 345,
        #endregion
        #region Others
        [Description("Object could not be encrypted")]
        Object_NotEncrypted = 346,
        [Description("Object could not be desencrypted")]
        Object_NotDesencrypted = 347,
        #endregion
        #endregion
        #region List Error Codes(350-399)
        #region Create
        [Description("Configurations could not be created")]
        Configurations_NotCreated = 350,
        [Description("Rooms could not be created")]
        Rooms_NotCreated = 351,
        [Description("Users could not be created")]
        Users_NotCreated = 352,
        [Description("Values could not be created")]
        Values_NotCreated = 353,
        [Description("HeadQuarters could not be created")]
        HQs_NotCreated = 354,
        [Description("Telegram Pushes could not be created")]
        TelegramPushes_NotCreated = 355,
        #endregion
        #region Read
        [Description("Configurations could not be found")]
        Configurations_NotFound = 360,
        [Description("Rooms could not be found")]
        Rooms_NotFound = 361,
        [Description("Values could not be found")]
        Values_NotFound = 362,
        [Description("Users could not be found")]
        Users_NotFound = 363,
        [Description("HeadQuarters could not be found")]
        HQs_NotFound = 364,
        [Description("Telegram Pushes could not be found")]
        TelegramPushes_NotFound = 365,
        #endregion
        #region Update
        [Description("Configurations could not be updated")]
        Configurations_NotUpdated = 370,
        [Description("Rooms could not be updated")]
        Rooms_NotUpdated = 371,
        [Description("Values could not be updated")]
        Values_NotUpdated = 372,
        [Description("Users could not be updated")]
        Users_NotUpdated = 373,
        [Description("HeadQuarters could not be updated")]
        HQs_NotUpdated = 374,
        [Description("Telegram Pushes could not be updated")]
        TelegramPushes_NotUpdated = 375,
        #endregion
        #region Delete
        [Description("Configurations could not be deleted")]
        Configurations_NotDeleted = 380,
        [Description("Rooms could not be deleted")]
        Rooms_NotDeleted = 381,
        [Description("values could not be deleted")]
        Values_NotDeleted = 382,
        [Description("Users could not be deleted")]
        Users_NotDeleted = 383,
        [Description("HeadQuarters could not be deleted")]
        HQs_NotDeleted = 384,
        [Description("Telegram Pushes could not be deleted")]
        TelegramPushes_NotDeleted = 385,
        #endregion
        #endregion
        #region Exception Codes (500>)
        [Description("Exception thrown in Create Method")]
        Exception_Create = 510,
        [Description("Exception thrown in Read Method")]
        Exception_Read = 511,
        [Description("Exception thrown in Update Method")]
        Exception_Update = 512,
        [Description("Exception thrown in Delete Method")]
        Exception_Delete = 513,
        [Description("Object encrypted successfully")]
        Object_Encryption = 514,
        #endregion
    }

    public static class ExtensionReponseCode
    {
        public static int GetCode(this ResponseCode response) => (int)response;
             
        public static string GetDescription(this ResponseCode response)
        {
            Type genericEnumType = response.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(response.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return response.ToString();
        }

    }
}
