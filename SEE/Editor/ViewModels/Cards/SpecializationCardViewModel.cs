using ScheduleData;

namespace Editor.ViewModels.Cards
{
    class SpecializationCardViewModel : HasProjectProperty
    {

        #region Properties

        #region Specialization

        private Specialization _specialization;
        public Specialization Specialization
        {
            get { return _specialization; }
            set
            {
                if (_specialization != value)
                {
                    _specialization = value;
                    RaisePropertyChanged(() => Specialization);
                }
            }
        }

        #endregion

        #endregion


        #region Ctor


        public SpecializationCardViewModel(Specialization specialization)
        {
            Specialization = specialization;
        }

        #endregion
    }
}
