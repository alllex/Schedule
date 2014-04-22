namespace DataModel

open System

type IGeoAddress =
    abstract member FullAddress : string

type WeekType = Odd | Even

type ITimeInterval =
    abstract member Week : WeekType  
    abstract member Begin : DateTime
    abstract member End : DateTime

type ISubject =
    abstract member Name : string

type IRoom =
    abstract member Name : string
    abstract member GeoAddress : IGeoAddress
    abstract member Capacity : int       
    abstract member Lectures : ILecture list

and ILecturer =
    abstract member Name : string
    abstract member Degree : string
    abstract member Lectures : ILecture list

and IGroup =
    abstract member Name : string
    abstract member Subgroups : IGroup list
    abstract member Lectures : ILecture list

and ILecture =
    abstract member Subject : ISubject
    abstract member Group : IGroup
    abstract member Room : IRoom
    abstract member Lecturer : ILecturer
    abstract member TimeInterval : ITimeInterval

type Table<'Subject> = // 'Subject can be either IGroup, IRoom or ILecturer
    abstract member GetLecture : ITimeInterval -> 'Subject -> ILecture option  // None  ~ there's no lecture at that time
    abstract member SetLecture : ITimeInterval -> 'Subject -> ILecture -> unit 
    abstract member HasSubject : 'Subject -> bool
    abstract member AddSubject : 'Subject -> bool               // false ~ already exists
    abstract member RemoveSubject : 'Subject -> bool            // false ~ already exists
    abstract member HasTimeInterval : ITimeInterval -> bool
    abstract member AddTimeInterval : ITimeInterval -> bool     // false ~ already exists
    abstract member RemoveTimeInterval : ITimeInterval -> bool  // false ~ already exists

type Schedule = 
    abstract member GroupSchedule : Table<IGroup>   
    abstract member LecturerSchedule : Table<ILecturer>
    abstract member RoomSchedule : Table<IRoom>



