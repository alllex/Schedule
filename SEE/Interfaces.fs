open System

type Time = int

type LessonType = Empty | Full | Partial

type IElementaryLesson =
    abstract member Name : string
    abstract member Lecturer : ILecturer
    abstract member Classroom : IClassroom
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
    abstract member Name : string
    abstract member Lesson : Time -> ILesson

and IClassroom =
    abstract member Number : int
    abstract member Address : string
    abstract member Projector : bool
    abstract member Computers : bool
    abstract member Lesson : Time -> ILesson

and IGroup =
    abstract member Identifier : string
    abstract member ExternalGroup : Option<IGroup>
    abstract member AddSubgroup : IGroup -> unit
    abstract member RemoveSubgroup : IGroup -> unit
    abstract member GetSubgroup : string -> IGroup
    abstract member Lesson : Time -> ILesson

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
    abstract member Undo : Operation -> unit
    abstract member Redo : Operation -> unit
