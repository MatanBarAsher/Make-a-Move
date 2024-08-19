using Make_a_move___Server.DAL;
using System;
namespace Make_a_move___Server.BL
{
    public class Place
    {
        private int placeCode;
        private string name;
        private string address;
        
        private string typeOfPlace;
        private static List<Place> placesList = new List<Place>();

        public Place() { }

        public Place(int placeCode, string name, string address, string typeOfPlace)
        {
            this.placeCode = placeCode;
            this.name = name;
            this.address = address;
           
            this.typeOfPlace = typeOfPlace;
        }

        public int PlaceCode { get => placeCode; set => placeCode = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        
        public string TypeOfPlace { get => typeOfPlace; set => typeOfPlace = value; }

        public int InsertPlace()
        {
            try
            {
                DBservicesPlace dbs = new DBservicesPlace();
                List<Place> places = dbs.ReadPlaces();
                Console.WriteLine(places.Count);
                int newCode = places[places.Count-1].placeCode + 1;
                this.placeCode = newCode;
                placesList.Add(this);
                return dbs.InsertPlace(this);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error inserting place", ex);
            }
        }

        public List<Place> ReadPlaces()
        {
            try
            {
                DBservicesPlace dbs = new DBservicesPlace();
                return dbs.ReadPlaces();
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error reading places", ex);
            }
        }

        public Place UpdatePlace(Place newplace)
        {
            try
            {
                DBservicesPlace dbs1 = new DBservicesPlace();
                List<Place> list = dbs1.ReadPlaces();
                // Find the Place in the PlacesList by PlaceCode
                Place placeToUpdate = list.Find(p => p.PlaceCode.Equals(newplace.PlaceCode));

                if (placeToUpdate != null)
                {
                    // Update user information
                    placeToUpdate.Name = newplace.Name;
                    placeToUpdate.PlaceCode = newplace.PlaceCode;
                    placeToUpdate.Address = newplace.Address;
                  
                    placeToUpdate.TypeOfPlace = newplace.TypeOfPlace;

                    // Update in the database (assuming DBservices has an UpdatePlace method)
                    DBservicesPlace dbs = new DBservicesPlace();
                    return dbs.UpdatePlace(placeToUpdate);
                }
                else
                {
                    // Place not found, handle the case appropriately (return null, throw an exception, etc.)
                    return null; // Or throw new Exception("Place not found");
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error updating Place", ex);
            }
        }

        public int checkExistingPlaceByName(string name)
        {
            // Instantiate the DBservicesPlace to access the ReadPlaces method
            DBservicesPlace dbs1 = new DBservicesPlace();

            // Retrieve the list of places from the database
            List<Place> places = dbs1.ReadPlaces();

            // Check if any place in the list has the given name
            foreach (var place in places)
            {
                if (place.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    // If a matching place is found, return 1
                    return place.placeCode;
                }
            }

            // If no matching place is found, add a new place and return 0
            int newPlaceCode = places.Count + 1;
            DateTime now = new DateTime();
            Place newPlace = new Place(newPlaceCode, name, "", "");
            dbs1.InsertPlace(newPlace);
            return newPlace.placeCode;
        }





    }
}
