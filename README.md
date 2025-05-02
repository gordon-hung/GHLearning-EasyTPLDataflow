# GHLearning-EasyTPLDataflow

工作平行程式庫 (TPL) 提供資料流程元件，協助讓啟用並行的應用程式更強固。 這些資料流程元件合稱為「TPL 資料流程程式庫」。 此資料流程模型以提供針對廣泛資料流程以及管線工作的同處理序訊息傳遞，將以行動為基礎的程式撰寫升級。 資料流程元件會在 TPL 的類型與排程基礎結構上建置，並整合 C#、Visual Basic 以及 F# 語言對非同步程式設計的支援。 當您有多個必須非同步式互相溝通的作業時，或當您因為資料變為可用而要處理資料時，這些資料流程元件就會相當實用。 例如，請考慮一個應用程式，它會處理來自網路攝影機的影像資料。 使用資料流模型，應用程式就可以在影像畫面可用時處理它們。 例如，如果應用程式因執行光源修正或消除紅眼而增強影像畫面，則您可以建立資料流程元件的「管線」。 此管線的每個階段都可能使用更廣泛的平行處理原則功能 (例如 TPL 提供的功能) 來轉換該影像。

### BufferBlock<T>
BufferBlock<T> 類別代表一般用途的非同步傳訊結構。 這個類別會儲存可由多個來源寫入或由多個目標讀取之訊息的先進先出 (FIFO) 佇列。 當目標從 BufferBlock<T> 物件接收到訊息時，就會從訊息佇列中移除該訊息。 因此，雖然 BufferBlock<T> 物件可以具有多個目標，只有一個目標會接收到每則訊息。 當您想要將多則訊息傳遞給其他元件，而且該元件必須接收每則訊息時，BufferBlock<T> 類別就很有用。

### BroadcastBlock<T>
當您需要將多則訊息傳遞給其他元件，但是該元件只需要最新的值時，BroadcastBlock<T> 類別就很有用。 當您想要將訊息廣播至多個元件時，這個類別也很有用。

### ActionBlock<T>
ActionBlock<TInput> 類別是在接收到資料時呼叫委派的目標區塊。 可將 ActionBlock<TInput> 物件視為在資料可用時會非同步執行的委派。 您提供給 ActionBlock<TInput> 物件的委派可以是 Action<T> 類型或 System.Func<TInput, Task> 類型。 當您搭配 Action<T> 使用 ActionBlock<TInput> 物件時，會將每個輸入項目的處理在委派傳回時視為完成。 當您搭配 System.Func<TInput, Task> 使用 ActionBlock<TInput> 物件時，只有在傳回的 Task 物件已完成時，才會將每個輸入項目的處理視為已完成。 使用這兩種機制，您可以使用 ActionBlock<TInput> 為每個輸入項目作同步與非同步處理。

### TransformBlock<TInput, TOutput>
TransformBlock<TInput,TOutput> 類別類似於 ActionBlock<TInput> 類別，不同處在於它可同時作為來源和目標。 您傳遞給 TransformBlock<TInput,TOutput> 物件的委派會傳回類型 TOutput 的值。 您提供給 TransformBlock<TInput,TOutput> 物件的委派可以是類型 System.Func<TInput, TOutput> 或類型 System.Func<TInput, Task<TOutput>>。 當您搭配 System.Func<TInput, TOutput> 使用 TransformBlock<TInput,TOutput> 物件時，會將每個輸入項目的處理在委派傳回時視為完成。 當您搭配 System.Func<TInput, Task<TOutput>> 使用 TransformBlock<TInput,TOutput> 物件時，只有在傳回的 Task<TResult> 物件已完成時，才會將每個輸入項目的處理視為已完成。 如同 ActionBlock<TInput>，使用這兩種機制，您就可以使用 TransformBlock<TInput,TOutput> 為每個輸入項目作同步與非同步處理。

### BatchBlock<T>
BatchBlock<T> 類別將稱為批次的輸入資料集合併為輸出資料陣列。 在建立 BatchBlock<T> 物件時，請指定每一批次的大小。 當 BatchBlock<T> 物件接收指定的輸入項目計數時，會非同步散佈包含這些項目的陣列。 如果 BatchBlock<T> 物件設定為完成狀態，但未包含足夠構成批次的項目，則會散佈包含剩餘輸入項目的最後一個陣列。

BatchBlock<T> 類別會在窮盡或非窮盡模式下運作。 在窮盡模式 (這是預設值)，BatchBlock<T> 物件接受每則提供的訊息，並在接收指定的項目計數後散佈陣列。 在非窮盡模式，BatchBlock<T> 物件延後所有傳入訊息，直到來源提供給區塊的訊息足以形成批次。 因為窮盡模式需要較少的處理額外負荷，通常其效能優於非窮盡模式。 不過，在您必須以不可部分完成的方式協調來自多個來源之消耗時，可以使用非窮盡模式。 在 BatchBlock<T> 建構函式的 dataflowBlockOptions 參數中，設定 Greedy 為 False，來指定非窮盡模式。

### Rx（Reactive Extensions
Rx（Reactive Extensions）是一個用於處理異步和事件基礎編程的庫。它擴展了 LINQ 的功能，使得開發者能夠更簡單地處理異步數據流和事件流。Rx 提供了一個“觀察者模式”的實現，使得事件和數據流可以像集合一樣被處理和操作。

### 資料來源
- [資料流程 (工作平行程式庫)](https://learn.microsoft.com/zh-tw/dotnet/standard/parallel-programming/dataflow-task-parallel-library)
- [Rx.NET GitHub Repository](https://github.com/dotnet/reactive)