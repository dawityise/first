using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
namespace MangoDbDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoServer mongo = MongoServer.Create(); 
            mongo.Connect();  
            var db = mongo.GetDatabase("test");
            //using (mongo.RequestStart(db))
            //{
                var collection = db.GetCollection<BsonDocument>("books");
                BsonDocument d = new BsonDocument()
                   //.Add("_id", BsonValue.Create(BsonType.ObjectId))
                   .Add("author", "Ernest Hemingway")
                   .Add("title", "For Whom the Bell Tolls")
                   .Add("address", new BsonDocument()
                               .Add("street", "123 Main St.")
                               .Add("city", "Centerville")
                               .Add("state", "PA")
                               .Add("zip", 12345));

                collection.Insert(d);
               // var foo = collection.FindAll();
                var query = new QueryDocument("author", "Ernest Hemingway");

                foreach (BsonDocument item in collection.Find(query)) 
                {
                    BsonElement author = item.GetElement("author");
                    BsonElement title = item.GetElement("title");
                    BsonElement address = item.GetElement("address");

                    Console.WriteLine("Author: {0}, Title: {1}, Address: {2}", 
                        author.Value, title.Value, address.Value); 
                    foreach (BsonElement element in item.Elements)
                    {
                        Console.WriteLine("FieldName: {0}, FieldValue: {1}", element.Name, element.Value);
                    }
                }
                Console.ReadLine();
            //}
            
            mongo.Disconnect(); 
        }
        static void AddNewData(ref MongoCollection<BsonDocument> collection)
        {
            BsonDocument d = new BsonDocument()
                    //.Add("_id", BsonValue.Create(BsonType.ObjectId))
                    .Add("author", "Dawit Abebe")
                    .Add("title", "For Whom the Bell Tolls")
                    .Add("address", new BsonDocument()
                                .Add("street", "123 Main St.")
                                .Add("city", "Centerville")
                                .Add("state", "PA")
                                .Add("zip", 12345));

            collection.Insert(d);
        }
    }
}
