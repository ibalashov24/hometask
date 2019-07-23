module Main 

open System.Net
open System.IO
open System.Text.RegularExpressions

/// Downloads page with given url and all subpages
let downloadPages url =
    /// Downloads page with given url
    let fetchUrlAsync (url : string) = 
        async {
            try
                let request = WebRequest.Create(url)
                use! response = request.AsyncGetResponse()
                use stream = response.GetResponseStream()
                use reader = new StreamReader(stream)
                let html = reader.ReadToEnd()
                do printfn "%s --- %d" url html.Length

                return Some html
            with
                | _ -> 
                    do printfn "%s is not available!" url
                    return None
        }

    /// Downloads pages and subpages
    let downloader (pageString : string) =
        let regex = Regex(
                        "<a href\s*=\s*\"?(https?://[^\"]+)\"?\s*>", 
                        RegexOptions.IgnoreCase) 
        let matches = regex.Matches(pageString)
        let nextPages = [for url in matches -> 
                            url.Groups.[1].Value |> fetchUrlAsync]
        Async.Parallel nextPages |> Async.RunSynchronously |> Array.toList


    let initPageString = url |> fetchUrlAsync |> Async.RunSynchronously
    match initPageString with
    | None -> [None]
    | Some page -> 
        initPageString :: downloader page
        