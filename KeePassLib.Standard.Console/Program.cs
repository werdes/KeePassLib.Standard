
using KeePassLib.Keys;
using KeePassLib.Serialization;


namespace KeePassLib.Standard.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateDb();
        }

        private static string CreateDb()
        {
            string path = "TestOutput.kdbx";
            PwDatabase db = new PwDatabase();
            CompositeKey compositeKey = new CompositeKey();
            compositeKey.AddUserKey(new KcpPassword("Test"));

            IOConnectionInfo ioc = IOConnectionInfo.FromPath(path);
            db.New(ioc, compositeKey);

            PwEntry entry = new PwEntry(true, true);
            entry.Strings.Set(PwDefs.TitleField, new Security.ProtectedString(db.MemoryProtection.ProtectTitle, "Test-Titel"));
            entry.Strings.Set(PwDefs.UserNameField, new Security.ProtectedString(db.MemoryProtection.ProtectUserName, "Test-User"));
            entry.Strings.Set(PwDefs.PasswordField, new Security.ProtectedString(db.MemoryProtection.ProtectPassword, "Test-Password"));

            db.RootGroup.Entries.Add(entry);
            db.Save(null);

            return path;
        }
    }
}