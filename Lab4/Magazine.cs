using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4
{
    
       class Magazine : Edition, IRateAndCopy, IEnumerable
        {

            private Frequency _frequency;
            private List<Article> _articles;
            private List<Person> _editors;

            public Magazine(string magazineName, Frequency frequency, DateTime releaseDate, int copiesCount)
            {
                _name = magazineName;
                _frequency = frequency;
                _releaseDate = releaseDate;
                _copiesCount = copiesCount;
                _articles = new List<Article>();
                _editors = new List<Person>();
            }

            public Magazine()
            {
                _name = default;
                _frequency = default;
                _releaseDate = default;
                _copiesCount = default;
                _articles = default;
                _editors = default;
            }

            public Frequency Frequency
            {
                get => _frequency;
                set => _frequency = value;
            }



            public double Rating
            {
                get
                {
                    if (_articles.Count == 0) return 0;

                    return (from Article article in _articles
                            select article.Rating)
                        .Average();
                }
            }
            public bool this[Frequency frequency] => frequency == this._frequency;

            public List<Article> Articles
            {
                get => _articles;
                set => _articles = value;
            }

            public List<Person> Editors
            {
                get => _editors;
                set => _editors = value;
            }

            public Edition Edition
            {
                get => this;
                set
                {
                    _name = value.Name;
                    _releaseDate = value.ReleaseDate;
                    _copiesCount = value.CopiesCount;
                }
            }

            public IEnumerable ArticlesMoreThan(double rate)
            {
                return (from Article article in _articles select article)
                    .Where(article => article != null && article.Rating > rate);
            }

            public IEnumerable ArticlesWithName(string name)
            {
                return (from Article article in _articles select article)
                    .Where(article => article.ArticleName.Contains(name));
            }

            public void AddArticles(params Article[] articles)
            {
                foreach (Article article in articles)
                {
                    _articles.Add(article);
                }
            }

            public void AddEditors(params Person[] editors)
            {
                foreach (Person editor in editors)
                {
                    _editors.Add(editor);
                }
            }

            public override string ToString()
            {
                return "MagazineName: " + _name + "; Frequency: " + _frequency.ToString() + "; ReleaseDate: " +
                    _releaseDate.ToShortDateString() + "; CopiesCount: " + _copiesCount.ToString() + "; Articles: { " +
                    string.Join(", ", (from Article article in _articles select article.ToString()).ToArray()) + " }; Editors: { " +
                    string.Join(", ", (from Person editor in _editors select editor.ToString()).ToArray()) + " };";
            }

            public virtual string ToShortString()
            {
                return "MagazineName: " + _name + "; Frequency: " + _frequency.ToString() + "; ReleaseDate: " +
                    _releaseDate.ToShortDateString() + "; CopiesCount: " + _copiesCount.ToString() + "; AvgRate: " + Rating;
            }


            public override object DeepCopy()
            {
                Magazine magazine = new Magazine(_name, _frequency, new DateTime(_releaseDate.Year,
                    _releaseDate.Month, _releaseDate.Day), _copiesCount)
                {
                    Articles = new List<Article>((from Article article in _articles select (Article)article.DeepCopy()).ToArray()),
                    Editors = new List<Person>((from Person person in _editors select person.DeepCopy()).ToArray())
                };
                return magazine;
            }

            public IEnumerator GetEnumerator()
            {
                return new MagazineEnumerator(_articles);
            }

            public IEnumerator AuthorsWhichAreEditors()
            {
                IEnumerator enumerator = GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (_editors.Contains(enumerator.Current))
                    {
                        yield return enumerator.Current;
                    }
                }
            }

            public IEnumerator AuthorsWhoAreNotEditors()
            {
                IEnumerator enumerator = GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (!_editors.Contains(enumerator.Current))
                    {
                        yield return enumerator.Current;
                    }
                }
            }
        }
    }

