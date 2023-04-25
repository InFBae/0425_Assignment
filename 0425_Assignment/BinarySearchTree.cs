using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    internal class BinarySearchTree<T> where T : IComparable<T>
    {
        private Node root;
        public BinarySearchTree() { root = null; }
        public bool Add(T item)
        {
            Node newNode = new Node(item);
            if(item == null)
                throw new ArgumentNullException("item");
            // root가 없을 경우
            if(root == null)
            {
                root = newNode;
                return true;
            }

            Node current = root;
            while(current != null){
                // current Node의 Value가 newNode의 Value 보다 클 때 왼쪽 자식 쪽으로 이동
                if(current.Value.CompareTo(newNode.Value) > 0)
                {
                    // 왼쪽 자식이 있다면 왼쪽 자식과 비교
                    if(current.HasLeftChild)
                    {
                        current = current.left;
                    }
                    // 없다면 새 노드를 왼쪽 자식으로 추가 후 성공 리턴 
                    else
                    {
                        current.left = newNode;
                        newNode.parent = current;
                        return true;
                    }
                }
                // current Node의 Value가 newNode의 Value 보다 작을 때 오른쪽 자식 쪽으로 이동
                else if (current.Value.CompareTo(newNode.Value) < 0)
                {
                    // 오른쪽 자식이 있다면 왼쪽 자식과 비교
                    if (current.HasRightChild)
                    {
                        current = current.right;
                    }
                    // 없다면 새 노드를 왼쪽 자식으로 추가 후 성공 리턴 
                    else
                    {
                        current.right = newNode;
                        newNode.parent = current;
                        return true;
                    }
                }
                // 중복 값일 경우 추가 실패 리턴
                else
                {
                    return false;
                }
            }
            // 왜 써야하지?
            return true;
        }

        public bool Remove(T item) 
        { 
            if (item  == null) throw new ArgumentNullException("item");
            if(root == null) { return false; }

            Node findNode = FindNode(item);
            if(findNode != null)
            {
                EraseNode(findNode);
                return true;
            }
            else { return false; }
        }
        public Node FindNode(T item) 
        {
            Node current = root;
            while(current != null)
            {
                // 현재 값보다 찾는 값이 작을 경우
                if (current.Value.CompareTo(item) > 0)
                {
                    if(current.HasLeftChild)
                    {
                        current = current.left;
                    }
                }
                // 현재 값보다 찾는 값이 클 경우
                else if (current.Value.CompareTo(item) < 0)
                {
                    if(current.HasRightChild)
                    {
                        current = current.right;
                    }
                }
                // 일치할 경우
                else { return current; }
            }
            // 못 찾은 경우
            return null;
        }

        private void EraseNode(Node node)
        {
            // 자식이 하나도 없을 때
            if (node.HasNoChild)
            {
                // 루트 노드인 경우
                if (node.IsRootNode)
                {
                    root = null;
                }
                else
                {
                    // 왼쪽 자식인 경우
                    if (node.IsLeftChild)
                    {
                        node.parent.left = null;
                    }
                    // 오른쪽 자식인 경우
                    else if (node.IsRightChild)
                    {
                        node.parent.right = null;
                    }
                }

            }
            // 자식이 하나만 있을 경우
            else if(node.HasLeftChild ^ node.HasRightChild)
            {
                Node child = node.HasLeftChild ? node.left : node.right;
                // 루트 노드인 경우
                if (node.IsRootNode)
                {
                    root = child;
                    child.parent = null;
                }
                else
                {
                    // 왼쪽 자식인 경우
                    if (node.IsLeftChild)
                    {
                        node.parent.left = child;
                        child.parent = node.parent;
                    }
                    // 오른쪽 자식인 경우
                    else if (node.IsRightChild)
                    {
                        node.parent.right = child;
                        child.parent = node.parent;
                    }
                }
            }
            // 자식을 둘 모두 가지고 있는 경우
            // 왼쪽 자식의 가장 오른쪽 자식 또는 오른쪽 자식의 가장 왼쪽 자식을 데려온다
            else
            {
                Node replaceNode = node.left;
                while( replaceNode.HasRightChild)
                {
                    replaceNode = replaceNode.right;
                }
                // 루트 노드인 경우
                if(node.IsRootNode)
                {
                    root = replaceNode;
                    replaceNode.parent = null;
                }
                else
                {
                    // 왼쪽 자식인 경우
                    if (node.IsLeftChild)
                    {
                        node.parent.left = replaceNode;
                        replaceNode.parent = node.parent;
                    }
                    // 오른쪽 자식인 경우
                    else if (node.IsRightChild)
                    {
                        node.parent.right = replaceNode;
                        replaceNode.parent = node.parent;
                    }
                }
            }
        }

        public void Print(Node node)
        {
            if (node.HasLeftChild)
            {
                Print(node.left);
            }
            Console.Write($"{node.Value}\t");
            if (node.HasRightChild)
            {
                Print(node.right);
            }
            //Console.WriteLine();
        }
        public void Print()
        {
            Print(root);
        }
        public void Clear()
        {
            root = null;
        }

        public void Refactoring()
        {
            Refactoring(root);
        }
        private void Refactoring(Node node)
        {
            Node current = node;

            CheckBF(current);

            if (current.HasLeftChild)
            {
                current = current.left;
                Refactoring(current);
            }
            if (current.HasRightChild)
            {
                current = current.right;
                Refactoring(current);
            }
            
        }
        private void CheckBF(Node node)
        {
            if (node.BF > 1)
            {
                RightRotation(node);
                return;
            }
            else if (node.BF < -1)
            {
                LeftRotation(node);
                return;
            }
        }
        private void RightRotation(Node node)
        {
            if (node.IsRootNode)
            {
                Node target = node.left;
                Node temp = target.right;

                target.right = node;
                target.parent = node.parent;
                node.parent = target;
                node.left = temp;
                root = target;
            }
            else
            {
                if(node.IsLeftChild) 
                {
                    Node target = node.left;
                    Node temp = target.right;

                    target.right = node;
                    target.parent = node.parent;
                    node.parent = target;
                    node.left = temp;
                }
                else if(node.IsRightChild)
                {
                    Node target = node.left;
                    Node temp = target.right;

                    target.right = node;
                    target.parent = node.parent;
                    node.parent = target;
                    node.left = temp;
                }
            }
        }
        private void LeftRotation(Node node)
        {
            if (node.IsRootNode)
            {
                Node target = node.right;
                Node temp = target.left;

                target.left = node;
                node.parent = target;
                node.right = temp;
                target.parent = null;
                root = target;
            }
            else
            {
                if (node.IsLeftChild)
                {
                    Node target = node.right
                    Node temp = node.right.left;
                    node.right.left = node;
                    node.parent = node.left;
                    node.left.parent = node.parent;
                    node.parent.left = node.left;
                }
                else if (node.IsRightChild)
                {

                }
            }
        }

        public class Node
        {
            internal T value;
            internal Node parent;
            internal Node left;
            internal Node right;

            public int BF { get { return LeftChildHeight - RightChildHeight; } }   // 밸런스 팩터

            // LeftChildHeight 구하기
            public int LeftChildHeight
            {
                get
                {
                    int count = 0;
                    Node current = this;
                    while (current != null)
                    {
                        if (current.HasLeftChild)
                        {
                            count++;
                            current = current.left;
                        }
                    }
                    return count;
                }
            }
            // RightChildHeight 구하기
            public int RightChildHeight
            {
                get
                {
                    int count = 0;
                    Node current = this;
                    while (current != null)
                    {
                        if (current.HasRightChild)
                        {
                            count++;
                            current = current.right;
                        }
                    }
                    return count;
                }
            }
            public T Value { get { return value; } }
            public Node(T value) 
            { 
                this.value = value;
                this.parent = null;
                this.left = null;
                this.right = null;
            }
            public bool HasNoChild { get { return left == null && right == null; } }
            public bool HasLeftChild { get { return left != null; } }
            public bool HasRightChild { get { return right != null; } }
            public bool IsLeftChild { get { return parent.left == this; } }
            public bool IsRightChild { get { return parent.right == this; } }
            public bool IsRootNode { get { return parent == null; } }
        }
    }
}
