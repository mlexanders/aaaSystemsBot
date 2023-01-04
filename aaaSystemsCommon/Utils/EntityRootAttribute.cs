namespace aaaSystemsCommon.Utils
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EntityRootAttribute : Attribute
    {
        public string Root { get; set; }

        public EntityRootAttribute(string root)
        {
            Root = root;
        }
    }
}
