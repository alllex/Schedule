open System

type Day = Monday | Tuesday | Wednesday | Thursday | Friday | Saturday

type LessonNumber = int

type TimeLimitation = string

type LessonType = Empty | Full | Partial

type IElementaryLesson =
    abstract member Name : string with set, get
    abstract member Lecturer : ILecturer with set, get
    abstract member Classroom : IClassroom with set, get
    abstract member AddGroup : IGroup -> unit
    abstract member RemoveGroup : IGroup -> unit
    abstract member CheckGroup : IGroup -> bool
    abstract member ShowGroupList : unit -> List<IGroup>
    abstract member Day : Day with set, get
    abstract member LessonNumber : LessonNumber with set, get
    abstract member TimeLimitation : TimeLimitation with set, get

and ILesson =
    abstract member GetType : unit -> LessonType
    abstract member GetElementaryLesson : unit -> Option<IElementaryLesson>
    abstract member GetNumerator : unit -> Option<IElementaryLesson>
    abstract member GetDenominator : unit -> Option<IElementaryLesson>
    abstract member GetElementaryLessonList : unit -> List<IElementaryLesson>

and ILecturer =
    abstract member Name : string with set, get
    abstract member SetLesson : Day -> LessonNumber -> ILesson  -> unit
    abstract member GetLesson : Day -> LessonNumber -> ILesson

and IClassroom =
    abstract member Number : string with set, get
    abstract member Address : string with set, get
    abstract member Projector : bool with set, get
    abstract member Computers : bool with set, get
    abstract member SetLesson : Day -> LessonNumber -> ILesson  -> unit
    abstract member GetLesson : Day -> LessonNumber -> ILesson

and IGroup =
    abstract member Identifier : string with set, get
    abstract member ExternalGroup : Option<IGroup>
    abstract member AddSubgroup : IGroup -> unit
    abstract member RemoveSubgroup : IGroup -> unit
    abstract member GetSubgroup : string -> IGroup
    abstract member SetLesson : Day -> LessonNumber -> ILesson -> unit
    abstract member GetLesson : Day -> LessonNumber -> ILesson 

type ISchedule =
//  1
    abstract member SaveData : string -> unit
    abstract member LoadData : unit -> string
//  2
    abstract member AddGroup : IGroup -> unit
    abstract member RemoveGroup : IGroup -> unit
    abstract member AddLecturer : ILecturer -> unit
    abstract member RemoveLecturer : ILecturer -> unit
    abstract member AddClassroom : IClassroom -> unit
    abstract member RemoveClassroom : IClassroom -> unit
    abstract member GetGroup : string -> IGroup
    abstract member GetLecturer : string -> ILecturer
    abstract member GetClassroom : string -> IClassroom
//  3
    abstract member Undo : unit -> unit
    abstract member Redo : unit -> unit
