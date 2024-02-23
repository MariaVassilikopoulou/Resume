namespace MyBrandNewCV.Services.ServiceHelper
{
    public class ServiceHelper
    {
        
            public static List<T> ConvertToList<T>(IEnumerable<object> items) where T : class
            {
                List<T> resultList = new List<T>();

                foreach (var item in items)
                {
                    if (item is T typedItem)
                    {
                        resultList.Add(typedItem);
                    }
                }

                return resultList;
            }
        }

    }

