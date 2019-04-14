using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System.Reactive.Linq;
using Reactive.Bindings;
using System.Threading;
using System.Windows.Data;
using TwitterClient.Model;

namespace TwitterClient.ViewModels
{
    public class TweetTimeLineViewModel : BindableBase
    {
        //public Dictionary<String, ObservableCollection<Tweet>> Tweets { get; set; } = new Dictionary<String, ObservableCollection<Tweet>>();
        public ObservableCollection<Tweet> Tweets { get; set; } = new ObservableCollection<Tweet>();
        
        public int Count { get; private set; } = 100;

        public DelegateCommand<object[]> UpdateCommand { get; private set; }

        public InteractionRequest<INotification> AlertRequest { get; } = new InteractionRequest<INotification>();

        private string account;
        public string Account
        {
            get { return this.account; }
            set { this.SetProperty(ref this.account, value); }
        }

        public TweetTimeLineViewModel()
        {
            this.Account = "LiSA_OLiVE";

            this.UpdateCommand = new DelegateCommand<object[]>(this.GetTweetsAsync);

            //this.Tweets.Add(new ObservableCollection<Tweet>());

            // 複数スレッドからコレクション操作できるようにする
            BindingOperations.EnableCollectionSynchronization(Tweets/*[this.Account]*/, new object());
            
            //GetTweetsAsync("LiSA_OLiVE");
            GetTweetsAsync(50,"LiSA_OLiVE");
        }

        // 戻り値 bool の Task
        private async Task<bool> AddListAsync3()
        {
            await Task.Run(() => AsyncAddList());

            return true;
        }

        private async void AsyncAddTweet()
        {

            await Task.Run(() => AsyncAddList2());

            /* これは非同期で動く */
            //await Task.Run(() => {
            //    for (int i = 0; i < 100; i++)
            //    {
            //        //Tweets.Add(new Tweet($"naruko + {i}", "hello waorla"));
            //        Console.WriteLine("test");
            //        Thread.Sleep(10);
            //    }
            //});

        }
        // 
        // BindingOperations.EnableCollectionSynchronization(this.Tweets, new object());
        // でObservableCollectionにプロパティを指定しない場合は、下記のようにBeginInvokeを呼ぶ
        // 
        private Task AsyncAddList()
        {
            var act = new Action<int>((i) => { Tweets/*[Account]*/.Add(new Tweet($"naruko + {i}", "hello waorla", "")); });
            return Task.Run(() =>
            {
                for ( int i = 0; i < 1000; i++)
                {
                    App.Current.Dispatcher.BeginInvoke(act, new object[] { i });
                    Thread.Sleep(5000);
                }
            }
            );
        }

        private Task AsyncAddList2()
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Tweets/*[Account]*/.Add(new Tweet($"naruko + {i}", "hello waorla"));
                    Thread.Sleep(5000);
                }
            }
            );
        }

        // ↓引数が一つの場合は下記のように定義することで解決する
        //public DelegateCommand<string> UpdateCommand { get; private set; }
        //private async void GetTweetsAsync(string screenName)


        private async void GetTweetsAsync(object[] obj)
        {
            // 取得用パラメータ準備
            var cnt = (int)obj[0];
            var str = (String)obj[1];

            await Task.Run( () => GetTweetsAsync(cnt, str));
        }

        private async void GetTweetsAsync(int cnt, String str)
        {
            var API_Key = "**********************";
            var API_Secret = "**********************";
            var Access_token = "**********************";
            var Access_token_secret = "**********************";

            var tokens = CoreTweet.Tokens.Create(API_Key,
                API_Secret,
                Access_token,
                Access_token_secret);

            // 取得用パラメータ準備
            var parm = new Dictionary<string, object>();
            parm["count"] = cnt;
            parm["screen_name"] = str;

            try
            {
                // Tweet 取得(非同期版)
                var tweets = await tokens.Statuses.UserTimelineAsync(parm);

                var mediaTweets = tweets.Where(tw => tw.Entities?.Media != null);

                // ここも非同期処理
                await Task.Run(
                    () =>
                    {
                        foreach (var tweet in tweets)
                        {
                            Tweets/*[Account]*/.Add(new Tweet(tweet.User.ScreenName, tweet.Text, tweet.Entities?.Media?[0].MediaUrl));
                            Thread.Sleep(100);
                        }
                    }
                );

            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e.ToString());
                throw;
            }
        }






    }

}
