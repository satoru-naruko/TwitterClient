using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;

namespace TwitterClient.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _reginManager;
        public DelegateCommand<string> NavigateCommand { get; set; }

        public DelegateCommand AlertCommand { get; }
        public DelegateCommand InputCommand { get; }
        /// <summary>
        /// 大文字に変換
        /// </summary>
        private void InputCommandExecute()
        {
            this.Input = this.Input.ToUpper();
        }


        /// <summary>
        /// 何か入力されてたら実行可能
        /// </summary>
        /// <returns></returns>
        private bool CanConvertExecute()
        {
            return !string.IsNullOrWhiteSpace(this.Input);
        }

        public InteractionRequest<INotification> AlertRequest { get; } = new InteractionRequest<INotification>();

        private string input;

        public string Input
        {
            get { return this.input; }
            set { this.SetProperty(ref this.input, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _reginManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);

            this.AlertCommand = new DelegateCommand(() =>
                this.AlertRequest.Raise(new Notification { Title = "Alert", Content = this.Input + "mario" }));

            // 変換コマンドに実際の処理をわたして初期化
            this.InputCommand = new DelegateCommand(
                this.InputCommandExecute,
                this.CanConvertExecute);
        }

        private void Navigate(string uri)
        {
            _reginManager.RequestNavigate("ContentRegion", uri);
        }

    }
}
