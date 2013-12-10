open System

type Time = int

type LessonType = Empty | Full | Partial

type IElementaryLesson =
    abstract member Name : string with set, get
    abstract member Lecturer : ILecturer with set, get
    abstract member Classroom : IClassroom with set, get
    abstract member AddGroup : IGroup -> unit
    abstract member RemoveGroup : IGroup -> unit
    abstract member CheckGroup : IGroup -> bool
    abstract member ShowGroupList : unit -> List<IGroup>
//  (#7 ?)

and ILesson =
    abstract member GetType : unit -> LessonType
    abstract member GetElementaryLesson : unit -> Option<IElementaryLesson>
    abstract member GetNumerator : unit -> IElementaryLesson
    abstract member GetDenominator : unit -> IElementaryLesson

and
    ILecturer =
    abstract member Name : string with set, get
    abstract member SetLesson : Time -> ILesson 
    abstract member GetLesson : Time -> ILesson

and IClassroom =
    abstract member Number : int with set, get
    abstract member Address : string with set, get
    abstract member Projector : bool with set, get
    abstract member Computers : bool with set, get
    abstract member SetLesson : Time -> ILesson 
    abstract member GetLesson : Time -> ILesson

and IGroup =
    abstract member Identifier : string with set, get
    abstract member ExternalGroup : Option<IGroup>
    abstract member AddSubgroup : IGroup -> unit
    abstract member RemoveSubgroup : IGroup -> unit
    abstract member GetSubgroup : string -> IGroup
    abstract member SetLesson : Time -> ILesson 
    abstract member GetLesson : Time -> ILesson 

type ISchedule =
//  1
    abstract member SaveData : ISchedule -> unit
    abstract member LoadData : unit -> ISchedule
//  2
    abstract member AddGroup : IGroup -> unit
    abstract member RemoveGroup : IGroup -> unit
    abstract member AddLecturer : ILecturer -> unit
    abstract member RemoveLecturer : ILecturer -> unit
    abstract member AddClassroom : IClassroom -> unit
    abstract member RemoveClassroom : IClassroom -> unit
//  3
    abstract member Undo : unit -> unit
    abstract member Redo : unit -> unit
