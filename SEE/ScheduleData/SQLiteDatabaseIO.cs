using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ScheduleData
{
    class SQLiteDatabaseIO
    {
    	private static void SaveSubjects(Schedule schedule, SQLiteCommand command)
    	{
            string create_table = @"
        		create table subjects
				(
					id integer primary key autoincrement,
					name text not null
				);";
			command.CommandText = create_table;
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
            string create_table = @"
        		create table specializations
				(
					id integer primary key autoincrement,
					name text not null
				);";
			command.CommandText = create_table;
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
            string create_table = @"
        		create table years_of_study
				(
					id integer primary key autoincrement,
					name text not null
				);";
			command.CommandText = create_table;
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
            string create_table = @"
        		create table lecturers
				(
					id integer primary key autoincrement,
					name text not null,
					degree text not null,
					department text not null
				);";
			command.CommandText = create_table;
            command.ExecuteNonQuery();

            foreach (var lecturer in schedule.Lecturers)
            {
            	string insert = "insert into lecturers (name, degree, department) values ('" + 
            					lecturer.Name + "', '" + 
            					lecturer.Degree + "', '" + 
            					lecturer.Department + "');";
				command.CommandText = insert;
	            command.ExecuteNonQuery();
            }
    	}
    	private static void SaveClassrooms(Schedule schedule, SQLiteCommand command)
    	{
            string create_table = @"
        		create table classrooms
				(
					id integer primary key autoincrement,
					name text not null,
					address text not null
				);";
			command.CommandText = create_table;
            command.ExecuteNonQuery();

            foreach (var classroom in schedule.Classrooms)
            {
            	string insert = "insert into classrooms (name, address) values ('" + 
            					classroom.Name + "', '" + 
            					classroom.Address + "');";
				command.CommandText = insert;
	            command.ExecuteNonQuery();
            }
    	}
    	private static void SaveTimeLine(Schedule schedule, SQLiteCommand command)
    	{
            string create_table = @"
        		create table time_line
				(
					id integer primary key autoincrement,
					day int not null,
					number int not null
				);";
			command.CommandText = create_table;
            command.ExecuteNonQuery();

            foreach (var classtime in schedule.TimeLine)
            {
            	string insert = "insert into time_line (day, number) values (" + 
            					(int)classtime.Day + ", " + 
            					classtime.Number + ");";
				command.CommandText = insert;
	            command.ExecuteNonQuery();
            }
    	}
    	private static void SaveGroups(Schedule schedule, SQLiteCommand command)
    	{
            string create_table = @"
        		create table groups
				(
					id integer primary key autoincrement,
					name text not null,
					spec int not null,
					year int not null
				);";
			command.CommandText = create_table;
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
            string create_table = @"
        		create table records
				(
					id integer primary key autoincrement,
					sub int not null,
					grp int not null,
					lect int not null,
					room int not null,
					time int not null
				);";
			command.CommandText = create_table;
            command.ExecuteNonQuery();

			for (int i = 0; i < schedule.ClassRecords.Count; i++)
            {
            	var record = schedule.ClassRecords[i];
                string insert = "insert into records (year, sub, grp, lect, room, time) values (" +
                                schedule.Subjects.IndexOf(record.Subject) + ", " +
                                schedule.Groups.IndexOf(record.Group) + ", " +
                                schedule.Lecturers.IndexOf(record.Lecturer) + ", " +
                                schedule.Classrooms.IndexOf(record.Classroom) + ", " +
                                schedule.TimeLine.IndexOf(record.ClassTime) + ");";
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
        public static void Save(Schedule schedule, string path)
        {
            SQLiteConnection.CreateFile(path);
            SQLiteConnection connection = new SQLiteConnection("data source=" + path);
            SQLiteCommand command = new SQLiteCommand(connection);
            connection.Open();

            string drop_all_tables = "select 'drop table ' || name || ';' from sqlite_master where type = 'table';";
            command.CommandText = drop_all_tables;
            command.ExecuteNonQuery();

            SaveSchedule(schedule, command);

            connection.Close();
        }

        private static void LoadSubjects(Schedule schedule, SQLiteCommand command)
        {
            command.CommandText = "select * from subjects";

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var subject = new Subject();
                subject.Name = (string)reader["name"];
                schedule.Subjects.Add(subject);
            }
            reader.Close();
        }

        private static void LoadSpecializations(Schedule schedule, SQLiteCommand command)
        {
            command.CommandText = "select * from specializations";

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var specialization = new Specialization();
                specialization.Name = (string)reader["name"];
                schedule.Specializations.Add(specialization);
            }
            reader.Close();
        }
        private static void LoadYearsOfStudy(Schedule schedule, SQLiteCommand command)
        {
            command.CommandText = "select * from years_of_study";

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var year = new YearOfStudy();
                year.Name = (string)reader["name"];
                schedule.YearsOfStudy.Add(year);
            }
            reader.Close();
        }
        private static void LoadLecturers(Schedule schedule, SQLiteCommand command)
        {
            command.CommandText = "select * from lecturers";

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var lecturer = new Lecturer();
                lecturer.Name = (string)reader["name"];
                lecturer.Degree = (string)reader["degree"];
                lecturer.Department = (string)reader["department"];
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
                var classroom = new Classroom();
                classroom.Name = (string)reader["name"];
                classroom.Address = (string)reader["address"];
                schedule.Classrooms.Add(classroom);
            }
            reader.Close();
        }
        private static void LoadTimeLine(Schedule schedule, SQLiteCommand command)
        {
            command.CommandText = "select * from time_line";

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var classtime = new ClassTime();
                classtime.Day = (Weekdays)reader["day"];
                classtime.Number = (int)reader["number"];
                schedule.TimeLine.Add(classtime);
            }
            reader.Close();
        }
        private static void LoadGroups(Schedule schedule, SQLiteCommand command)
        {
            command.CommandText = "select * from groups";

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var grp = new Group();
                grp.Name = (string)reader["name"];
                grp.Specialization = schedule.Specializations[(int)reader["spec"]];
                grp.YearOfStudy = schedule.YearsOfStudy[(int)reader["year"]];
                schedule.Groups.Add(grp);
            }
            reader.Close();
        }
        private static void LoadRecords(Schedule schedule, SQLiteCommand command)
        {
            command.CommandText = "select * from records";

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var record = new ClassRecord();
                record.Subject = schedule.Subjects[(int)reader["sub"]];
                record.Group = schedule.Groups[(int)reader["grp"]];
                record.Lecturer = schedule.Lecturers[(int)reader["lect"]];
                record.Classroom = schedule.Classrooms[(int)reader["room"]];
                record.ClassTime = schedule.TimeLine[(int)reader["time"]];
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
        public static Schedule Load(string path)
        {
            SQLiteConnection connection = new SQLiteConnection("data source=" + path);
            SQLiteCommand command = new SQLiteCommand(connection);
            connection.Open();

            var schedule = LoadSchedule(command);

            connection.Close();
            return schedule;
        }
    }
}
