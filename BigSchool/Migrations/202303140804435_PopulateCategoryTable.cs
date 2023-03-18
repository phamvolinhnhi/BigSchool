namespace BigSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategoryTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CATEGORIES (ID, NAME) VALUES (1, 'Business')");
            Sql("INSERT INTO CATEGORIES (ID, NAME) VALUES (2, 'Marketing')");
            Sql("INSERT INTO CATEGORIES (ID, NAME) VALUES (3, 'Developer')");
        }
        
        public override void Down()
        {
        }
    }
}
