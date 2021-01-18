# OpenCLPrac

- [__OpenCLの日本語版リファレンス__](http://neareal.net/index.php?Programming%2FOpenCL%2FJpnReference)
- [__参考書__](https://www.amazon.co.jp/C-%E3%81%AB%E3%82%88%E3%82%8BOpenCL%E4%B8%A6%E5%88%97%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0-%E5%8C%97%E5%B1%B1-%E6%B4%8B%E5%B9%B8/dp/4877832971)

CONCEPT: C# × OpenCL

---

## 1-1 プロフィールを表示するプログラム
### ●OpenCLPrac1_1_1

コンピュータ内のプラットフォームを列挙し, それぞれのプラットフォーム内のデバイスを列挙.

そしていくつかの情報を表示.

表示する情報は多数あるため, 適当に選択.

---

## 3-1 配列乗算プログラム
### ●OpenCLPrac3_1_1

配列Aと配列Bの各要素を乗算し, 配列Cの対応する要素に格納.

### ●OpenCLPrac3_1_2

[OpenCLPrac3_1_1](#openclprac3_1_1)をOpenCLを用いてプログラムした場合.


```fish
$ diff result/OpenCLPrac3_1_1.txt result/OpenCLPrac3_1_2.txt 
$ #何も表示されない
```
## 3-2 カーネルプログラムの分離
### ●OpenCLPrac3_2_1

ホストプログラムとカーネルプログラムを分離.

カーネルを変更したとしてもホストをリコンパイルする必要なし.

__cl__ フォルダはOpenCL共通ファイルが保存されている.

 | ファイル名     | 説明                                                                |
 | :------------- | :------------------------------------------------------------------ |
 | clConstants.cs | 共通ファイル. OPenCLに関する定数を定義.                             |
 | clStruct.cs    | 共通ファイル. OpenCLに関する構造体を定義.                           |
 | clDll.cs       | 共通ファイル. OpenCL APIに関する定義を行う.                         |
 | Program.cs     | 前節のMainに相当. <br>前節との相違はカーネルを外部から読み込むこと. |


\(カーネルは[kernel3_2_1](https://github.com/yosse95ai/OpenCLPrac/tree/master/kernel/kernel3_2_1)\)

## 3-3 データ並列
### ●OpenCLPrac3_3_1

これまでタスク並列だったものを, データ並列のプログラムに書き換えた.

カーネルとカーネル起動を要求するAPIがことなるだけ.

```fish
$ diff code/OpenCLPrac3_2_1.cs code/OpenCLPrac3_3_1.cs 
105c105,107
<         status = clEnqueueTask(queue, kernel, 0, null, IntPtr.Zero);
---
>         IntPtr[] globalSize = { (IntPtr)C.Length };
>         status = clEnqueueNDRangeKernel(queue, kernel, 1, null,
>             globalSize, null, 0, null, IntPtr.Zero);
```


\(カーネルは[kernel3_3_1](https://github.com/yosse95ai/OpenCLPrac/tree/master/kernel/kernel3_3_1)\)


データ並列で実行されるため, カーネルには各要素\(データ\)iに対する処理だけを記述.

このカーネルにiを与え, いくつも並列に動作させる.


## 3-4 カーネルの引数にコンスタント
### ●OpenCLPrac3_4_1

カーネルの引数にコンスタント(スカラ変数)を与える.

\(カーネルは[kernel3_4_1](https://github.com/yosse95ai/OpenCLPrac/tree/master/kernel/kernel3_4_1)\)


---
## 4-1 マネージプログラムとアンマネージプログラム間のデータ交換
### ●OpenCLPrac4_1_1
__C#とDLLのデータ型対応表__

| C#<br>マネージ | .NET Framework表現<br>マネージ | アンマネージ                  |
| :------------- | :----------------------------: | :---------------------------- |
| bool           |            Boolean             | int<br>long                   |
| byte           |              Byte              | unsigned char                 |
| char           |              Char              | char                          |
| short          |             Int16              | short                         |
| ushort         |             UInt16             | unsigned short                |
| int            |             Int31              | int<br> long                  |
| uint           |             UInt32             | unsigned int<br>unsigned long |
| long           |             Int64              | long long                     |
| float          |             Signal             | float                         |
| double         |             Double             | double                        |
| byte\[\]       |            Byte\[\]            | unsigned char*                |
| T\[\]          |             T\[\]              | T*                            |
| ulong          |             Uint64             | size_t                        |
| -              |       Text.StringBuilder       | char*                         |
| string         |             String             | const char*                   |

## 4-2 マネージからアンマネージへデータを渡す
### ●OpenCLPrac4_2_1

C#のChar型とC/C++のcharは異なる.

これ以降, [__dll_Source__](https://github.com/yosse95ai/OpenCLPrac/tree/master/dll_Source) にdllのもととなるC++のソースコードを入れる.

### ●OpenCLPrac4_2_2

`int`, `uint`, `short`, `long`について動作を確認.

### ●OpenCLPrac4_2_3

浮動小数点型をマネージからアンマネージへ渡す.

`float`, `Single`, `double`型を渡す.

## 4-3 アンマネージからマネージへデータを返す
### ●OpenCLPrac4_3_1

`ref`や`out`をつけた引数は __アドレス(ポインタ)渡し__ と等価.

| C/C++          | C#       |
| -------------- | -------- |
| unsigned char* | ref byte |
| int*           | ref int  |

## 4-4 文字列の受け渡し
### ●OpenCLPrac4_4_1

C/C++には文字列型が存在せず, 配列で処理している.

- 単に文字列をマネージからアンマネージへ渡す場合は`String`型
- アンマネージからマネージへ文字列を返す必要がある場合`StringBuilder`型

このコードでは文字コードとして`ANSI`を使用するので, DllImportに文字エンコード情報を明示的に`CharSet.Ansi`と指定.

もし異なるエンコードを使用する場合, 双方で使用する文字コードに合わせるとともに, C#側で文字セットを明記.

## 4-5 構造体の受け渡し
### ●OpenCLPrac4_5_1

マネージコードとアンマネージコード間ではメモリの変換が行われる.

C#の講座応対とDLLの構造体は同じメモリ配置でないといけないが, .NET Frameworkでは性能向上のためにCLRが各メンバを適当に配置する.

C#では単純に構造体を宣言しても, 各メンバの配置をプログラマが意識したように配置することは不可能.

C#の`StructLayout属性`に`LayoutKinf.Sequential`を指定して, メンバが宣言された順に配置されるようにC#に指示を出す.

DLL側の引数は構造体のポインタとする.

構造体をDLLに渡す場合, `ref`をつけて呼び出すことで, 情報を渡すことも受け取ることも可能になる.

## 4-6 配列の受け渡し
### ●OpenCLPrac4_6_1

配列を受け渡すことができれば, __バッファ__ を受け渡すこともできる.

他にも引数に「 __コールバック関数を渡す__」, 「 __Aliasを使用してDLL関数名を変更する__」, 「 __DLL関数をDEFファイルで定義する__」, 「 __DLLの変数をC#から調節参照する__」方法も考えられる.

---

## 5-1 APIをラップ
### ●OpenCLPrac5_1_1

DLL呼び出しを単にラップする.

| ファイル名          | 説明               |
|:------------------- | :----------------- |
| cl/clApiWrappers.cs | OpenCL APIラッパー |

を新規追加.

\(カーネルは[kernel5_1_1](https://github.com/yosse95ai/OpenCLPrac/tree/master/kernel/kernel5_1_1)\)

## 5-2 カーネルの引数にコンスタント
### ●OpenCLPrac5_2_1

前節と違い，変数Bは配列ではなくスカラ変数．

かつ，カーネルは変数Bを参照するだけで更新は行わないい．

このため，配列Aや配列Bのようにバッファオブジェクトを生成する必要はなく，変数を直接カーネルへわたす．

\(カーネルは[kernel5_2_1](https://github.com/yosse95ai/OpenCLPrac/tree/master/kernel/kernel5_2_1)\)

## 5-3 二次元配列
### ●OpenCLPrac5_3_1

二次元配列を扱う例．

`clEnqueueNDRangeKernel()`の第3引数に`2`を指定.

これは __2次元配列__ を処理することを意味する．

これに伴い第5引数である`globalSize`は要素数が2に代わる．

それぞれが要素の範囲を表す．

この値はカーネルの`get_global_id`組み込み関数と対応する．

---

## 移動平均
この章で紹介するプログラムは，単純なローパスフィルタ処理という感じ．

### ●移動平均とは
ある系列データを平滑化する方法．

一般的には，__株価の移動平均線__ や単純な __平滑化フィルタ__ に使われる．

単純な有限応答\(finite impulse response, FIR\)のローパスフィルタと考えるといい.

移動平均には，
- __単純移動平均__
- __加重移動平均__
- __指数移動平均__

の3種類が存在．

通常，移動平均といえば単純移動平均のことを指し，ここでも使う．

単純移動平均\(Simple Moving Average, SMA\)は，直近の __*n*__ 個の単純な平均を計算するだけ.

例えば，*w*この単純移動平均とは，直近の*w*個の平均．

つまり各データを D<sub>n</sub>とすると，この単純な移動平均を求める式は，

$$
(単純移動平均) = \frac{\sum_{n=0}^{w-1}D_n}{w}
$$

次の移動平均を求めるには，新たな数値を加え，一番古い数値を除くだけで計算できる．

$$
(次の移動平均値) = (直前の値) - \frac{D_0}{w} + \frac{D_w}{w}
$$

実際のプログラムでは，*w*で除算した値を加減算すると誤差が大きくなるため，総和に対して数値そのものを加減算し，その総和を*w*で除算する.

<div class="flow">
st=>$$\sum_{n=0}^{w-1}D_n$$: Start:>http://www.google.com[blank]
e=>end:>http://www.google.com
op1=>operation: My Operation
sub1=>subroutine: My Subroutine
cond=>condition: Yes
or No?:>http://www.google.com
io=>inputoutput: catch something...

st->op1->cond
cond(yes)->io->e
cond(no)->sub1(right)->op1
</div>