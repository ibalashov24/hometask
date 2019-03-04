namespace Problem2

module Main =
    /// Represents simple binary tree
    type BinaryTree<'T> = 
        | Tree of 'T * BinaryTree<'T> * BinaryTree<'T>
        | Tip

    /// Applies the 'mapping' function to the value of 
    /// each element in the given tree
    let rec binaryTreeMap<'T> (mapping : 'T -> 'T) (tree : BinaryTree<'T>) =
        match tree with
            | Tree(value, leftSon, rightSon) -> 
                Tree
                    (mapping value,
                    binaryTreeMap mapping leftSon, 
                    binaryTreeMap mapping rightSon)
            | Tip -> Tip

    let mapping = fun value -> value + 1
    let tree = 
        Tree
            (5, Tree(-1, Tip, Tip), Tree(56, Tree(-3, Tip, Tree(6, Tip, Tip)),
                Tree(0, Tree(5, Tip, Tip), Tip)))

    tree |> binaryTreeMap mapping |> printfn "%A"
