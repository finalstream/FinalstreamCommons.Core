# FinalstreamCommons

[![Build status](https://ci.appveyor.com/api/projects/status/27uk9s614srvn0dr?svg=true)](https://ci.appveyor.com/project/finalstream/finalstreamcommons)　[![NuGet](https://img.shields.io/nuget/v/FinalstreamCommons.svg?style=plastic)](https://www.nuget.org/packages/FinalstreamCommons/)　[![GitHub license](https://img.shields.io/github/license/finalstream/FinalstreamCommons.svg)]()

Windowsアプリケーション開発をサポートするフレームワーク＆ライブラリです。  
データベースはSQLiteに対応しています。

もともと[Movselex](http://www.finalstream.net/movselex/)向けに作成したものですが、
次世代のLinear Audio Playerにも採用する予定です。  
今後、Finalstreamで開発するアプリケーションのフレームワークとして精錬していく予定です。

フレームワークとしての使用方法等は近い将来オープンソースとして公開するMovselexのソースを確認していただければと思います。

##主な機能

###Framework
* AppClient(アプリケーションのクライント)
 * 設定ファイル(json)をサポート
 * スレッドセーフ処理実行機構(アクションを処理の単位として特定のスレッドで実行)
 * データベーススキーマアップグレード用アクション
* BackgroundWorker(バックグラウンド処理を実装を支援)

###Database
* DatabaseAccessor(データベースへのアクセスをサポート)
* SQLExecuter(Dapperのラッパ。SQLログ出力)
* SQLiteFunctions
 * GetDirectoryPathSQLiteFunction(ディレクトリパス取得)
 * GetFileSizeSQLiteFunction(ファイルサイズ取得)
 * JoinStringSQLiteFunction(string.joinをSQLで実現)

###Utils
* ColorUtils(反対色などを取得)
* DateUtils(シーズン取得)
* DialogUtils(ファイル選択、フォルダ選択ダイアログ)
* ScreenUtils(デバイス名からディスプレイデバイス情報取得)

###WebService
* GoogleCustomSearchService([Googleカスタム検索エンジン](https://cse.google.co.jp/cse/?hl=ja)の結果を取得)



