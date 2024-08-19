using Make_a_move___Server.DAL;
using System;
namespace Make_a_move___Server.BL
{
    public class TypeOfPlace
    {
        private int typeOfPlaceCode;
        private string typeOfPlaceDescription;
        private static List<TypeOfPlace> typeOfPlaceList = new List<TypeOfPlace>();

        public TypeOfPlace() { }
        public TypeOfPlace(int typeOfPlaceCode, string typeOfPlaceDescription)
        {
            this.typeOfPlaceCode = typeOfPlaceCode;
            this.typeOfPlaceDescription = typeOfPlaceDescription;
        }

        public int TypeOfPlaceCode { get => typeOfPlaceCode; set => typeOfPlaceCode = value; }
        public string TypeOfPlaceDescription { get => typeOfPlaceDescription; set => typeOfPlaceDescription = value; }

        public int InsertTypeOfPlace()
        {
            try
            {
                DBservicesTypeOfPlace dbs = new DBservicesTypeOfPlace();
                typeOfPlaceList.Add(this);
                return dbs.InsertTypeOfPlace(this);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error inserting typeOfPlace", ex);
            }
        }

        public List<TypeOfPlace> ReadTypeOfPlace()
        {
            try
            {
                DBservicesTypeOfPlace dbs = new DBservicesTypeOfPlace();
                return dbs.ReadTypeOfPlace();
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error reading typeOfPlace", ex);
            }
        }


        public TypeOfPlace UpdateTypeOfPlace(TypeOfPlace newType)
        {
            try
            {
                // Find the type in the TypesList by code
                DBservicesTypeOfPlace dbs1 = new DBservicesTypeOfPlace();
                List<TypeOfPlace> list = dbs1.ReadTypeOfPlace();
                TypeOfPlace typeToUpdate = list.Find(t => t.TypeOfPlaceCode.Equals(newType.TypeOfPlaceCode));


                if (typeToUpdate != null)
                {
                    // Update type information
                    typeToUpdate.TypeOfPlaceDescription = newType.TypeOfPlaceDescription;
                    typeToUpdate.TypeOfPlaceCode = newType.TypeOfPlaceCode;

                    // Update in the database (assuming DBservices has an UpdateType method)
                    DBservicesTypeOfPlace dbs = new DBservicesTypeOfPlace();
                    return dbs.UpdateTypeOfPlace(typeToUpdate);
                }
                else
                {
                    // Type not found, handle the case appropriately (return null, throw an exception, etc.)
                    return null; // Or throw new Exception("Type not found");
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error updating type", ex);
            }
        }




    }
}
