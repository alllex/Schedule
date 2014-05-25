using System.Data.SQLite;
using System.Linq;

namespace ScheduleData
{
    public class SQLiteDatabaseIO
    {

        #region Public

        public static void Save(Schedule schedule, string path)
        {
            SQLiteConnection.CreateFile(path);
            var connection = new SQLiteConnection("data source=" + path);
            var command = new SQLiteCommand(connection);
            connection.Open();

            const string dropAllTables = "select 'drop table ' || name || ';' from sqlite_master where type = 'table';";
            command.CommandText = dropAllTables;
            command.ExecuteNonQuery();

            SaveSchedule(schedule, command);

            connection.Close();
        }

        public static Schedule Load(string path)
        {
            var connection = new SQLiteConnection("data source=" + path);
            var command = new SQLiteCommand(connection);
            connection.Open();

            var schedule = LoadSchedule(command);

            connection.Close();
            return schedule;
        }

        #endregion

        #region Private

        private static void SaveSubjects(Schedule schedule, SQLiteCommand command)
    	{
            const string createTable = @"
        		create table subjects
				(
					id integer primary key autoincrement,
					name text not null
				);";
			command.CommandText = createTable;
            command.ExecuteNonQuery();

            foreach (var subject in schedule.Subjects)
            {
            	string insert = "insert into subjects (name) values ('" + 
            					subject.Name + "');";
				command.CommandText = insert;
	            command.ExecuteNonQuery();
            }
    	}
    	private static void SaveSpecializations(Schedule schedule, SQLiteCommand command)
    	{
            const string createTable = @"
        		create table specializations
				(
					id integer primary key autoincrement,
					name text not null
				);";
			command.CommandText = createTable;
            command.ExecuteNonQuery();

            foreach (var specialization in schedule.Specializations)
            {
            	string insert = "insert into specializations (name) values ('" + 
            					specialization.Name + "');";
				command.CommandText = insert;
	            command.ExecuteNonQuery();
            }
    	}
    	private static void SaveYearsOfStudy(Schedule schedule, SQLiteCommand command)
    	{
            const string createTable = @"
        		create table years_of_study
				(
					id integer primary key autoincrement,
					name text not null
				);";
			command.CommandText = createTable;
            command.ExecuteNonQuery();

            foreach (var year in schedule.YearsOfStudy)
            {
            	string insert = "insert into years_of_study (name) values ('" + 
            					year.Name + "');";
				command.CommandText = insert;
	            command.ExecuteNonQuery();
            }
    	}
    	private static void SaveLecturers(Schedule schedule, SQLiteCommand command)
    	{
            const string createTable = @"
        		create table lecturers
				(
					id integer primary key autoincrement,
					name text not null,
					degree text not null,
					department text not null
				);";
			command.CommandText = createTable;
            command.ExecuteNonQuery();

            foreach (var insert in schedule.Lecturers.Select(lecturer => "insert into lecturers (name, degree, department) values ('" + 
                                                                            lecturer.Name + "', '" + 
                                                                            lecturer.Degree + "', '" + 
                                                                            lecturer.Department + "');"))
            {
                command.CommandText = insert;
                command.ExecuteNonQuery();
            }
    	}
    	private static void SaveClassrooms(Schedule schedule, SQLiteCommand command)
    	{
            const string createTable = @"
        		create table classrooms
				(
					id integer primary key autoincrement,
					name text not null,
					address text not null
				);";
			command.CommandText = createTable;
            command.ExecuteNonQuery();

            foreach (var insert in schedule.Classrooms.Select(classroom => "insert into classrooms (name, address) values ('" + 
                                                                              classroom.Name + "', '" + 
                                                                              classroom.Address + "');"))
            {
                command.CommandText = insert;
                command.ExecuteNonQuery();
            }
    	}
    	private static void SaveTimeLine(Schedule schedule, SQLiteCommand command)
    	{
            const string createTable = @"
        		create table time_line
				(
					id integer primary key autoincrement,
					day int not null,
					number int not null
				);";
			command.CommandText = createTable;
            command.ExecuteNonQuery();

            foreach (var insert in schedule.TimeLine.Select(classtime => "insert into time_line (day, number) values (" + 
                                                                         (int)classtime.Day + ", " + 
                                                                         classtime.Number + ");"))
            {
                command.CommandText = insert;
                command.ExecuteNonQuery();
            }
    	}
    	private static void SaveGroups(Schedule schedule, SQLiteCommand command)
    	{
            const string createTable = @"
        		create table groups
				(
					id integer primary key autoincrement,
					name text not null,
					spec int not null,
					year int not null
				);";
			command.CommandText = createTable;
            command.ExecuteNonQuery();

            foreach (var grp in schedule.Groups)
            {
                string insert = "insert into groups (name, spec, year) values ('" + 
                				grp.Name + "', " + 
                				schedule.Specializations.IndexOf(grp.Specialization) + ", " + 
                				schedule.YearsOfStudy.IndexOf(grp.YearOfStudy) + ");";
				command.CommandText = insert;
	            command.ExecuteNonQuery();
            }
    	}
    	private static void SaveRecords(Schedule schedule, SQLiteCommand command)
    	{
            const string createTable = @"
        		create table records
				(
					id integer primary key autoincrement,
					sub int not null,
					grp int not null,
					lect int not null,
					room int not null,
					time int not null
				);";
			command.CommandText = createTable;
            command.ExecuteNonQuery();

			foreach (var insert in schedule.ClassRecords.Select(record => "insert into records (sub, grp, lect, room, time) values (" +
			                                                                 schedule.Subjects.IndexOf(record.Subject) + ", " +
			                                                                 schedule.Groups.IndexOf(record.Group) + ", " +
			                                                                 schedule.Lecturers.IndexOf(record.Lecturer) + ", " +
			                                                                 schedule.Classrooms.IndexOf(record.Classroom) + ", " +
			                                                                 schedule.TimeLine.IndexOf(record.ClassTime) + ");"))
			{
			    command.CommandText = insert;
			    command.ExecuteNonQuery();
			}
    	}
        private static void SaveSchedule(Schedule schedule, SQLiteCommand command)
        {
			SaveSubjects(schedule, command);
			SaveSpecializations(schedule, command);
			SaveYearsOfStudy(schedule, command);
			SaveLecturers(schedule, command);
			SaveClassrooms(schedule, command);
			SaveTimeLine(schedule, command);
            SaveGroups(schedule, command);
            SaveRecords(schedule, command);
        	
        }

        private static void LoadSubjects(Schedule schedule, SQLiteCommand command)
        {
            command.CommandText = "select * from subjects";

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var subject = new Subject {Name = (string) reader["name"]};
                schedule.Subjects.Add(subject);
            }
            reader.Close();
        }

        private static void LoadSpecializations(Schedule schedule, SQLiteCommand command)
        {
            command.CommandText = "select * from specializations";

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var specialization = new Specialization {Name = (string) reader["name"]};
                schedule.Specializations.Add(specialization);
            }
            reader.Close();
        }
        private static void LoadYearsOfStudy(Schedule schedule, SQLiteCommand command)
        {
            command.CommandText = "select * from years_of_study";

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var year = new YearOfStudy {Name = (string) reader["name"]};
                schedule.YearsOfStudy.Add(year);
            }
            reader.Close();
        }
        private static void LoadLecturers(Schedule schedule, SQLiteCommand command)
        {
            command.CommandText = "select * from lecturers";

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var lecturer = new Lecturer
                {
                    Name = (string) reader["name"],
                    Degree = (string) reader["degree"],
                    Department = (string) reader["department"]
                };
                schedule.Lecturers.Add(lecturer);
            }
            reader.Close();
        }
        private static void LoadClassrooms(Schedule schedule, SQLiteCommand command)
        {
            command.CommandText = "select * from classrooms";

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var classroom = new Classroom {Name = (string) reader["name"], Address = (string) reader["address"]};
                schedule.Classrooms.Add(classroom);
            }
            reader.Close();
        }
        private static void LoadTimeLine(Schedule schedule, SQLiteCommand command)
        {
            command.CommandText = "select * from time_line";

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var classtime = new ClassTime {Day = (Weekdays) reader["day"], Number = (int) reader["number"]};
                schedule.TimeLine.Add(classtime);
            }
            reader.Close();
        }
        private static void LoadGroups(Schedule schedule, SQLiteCommand command)
        {
            command.CommandText = "select * from groups";

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var grp = new Group
                {
                    Name = (string) reader["name"],
                    Specialization = schedule.Specializations[(int) reader["spec"]],
                    YearOfStudy = schedule.YearsOfStudy[(int) reader["year"]]
                };
                schedule.Groups.Add(grp);
            }
            reader.Close();
        }
        private static void LoadRecords(Schedule schedule, SQLiteCommand command)
        {
            command.CommandText = "select * from records";

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var record = new ClassRecord
                {
                    Subject = schedule.Subjects[(int) reader["sub"]],
                    Group = schedule.Groups[(int) reader["grp"]],
                    Lecturer = schedule.Lecturers[(int) reader["lect"]],
                    Classroom = schedule.Classrooms[(int) reader["room"]],
                    ClassTime = schedule.TimeLine[(int) reader["time"]]
                };
                schedule.ClassRecords.Add(record);
            }
            reader.Close();
        }
        private static Schedule LoadSchedule(SQLiteCommand command)
        {
        	var schedule = new Schedule();

			LoadSubjects(schedule, command);
			LoadSpecializations(schedule, command);
			LoadYearsOfStudy(schedule, command);
			LoadLecturers(schedule, command);
			LoadClassrooms(schedule, command);
			LoadTimeLine(schedule, command);
            LoadGroups(schedule, command);
            LoadRecords(schedule, command);
        	
        	return schedule;
        }

        #endregion
    }
}
