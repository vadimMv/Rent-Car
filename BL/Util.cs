using System;
using System.IO;
using System.Drawing;
namespace BL
{
    /// <summary>
    /// util static class 
    /// </summary>
    /// <typeparam name="TParent">First type</typeparam>
    /// <typeparam name="TChild">Second type</typeparam>
    public static class Util<TParent, TChild> where TParent : class where TChild : class
    {
        /// <summary>
        /// copy  all fields 2objects 
        /// </summary>
        /// <param name="parent">first object</param>
        /// <param name="child">second object</param>
        public static void Copy(TParent parent, TChild child)
        {
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();
            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType && parentProperty.GetValue(parent) != null)
                    {
                        childProperty.SetValue(child, parentProperty.GetValue(parent));
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// converting jpeg file to 64-base string
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string base64Img(string path) {
          
            using (Image image = Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

    }
}
