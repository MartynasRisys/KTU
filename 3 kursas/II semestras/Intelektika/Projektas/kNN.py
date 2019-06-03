import numpy as np
import pandas as pd
from sklearn.decomposition import PCA
import matplotlib.pyplot as plt
from sklearn import datasets, svm, metrics
from sklearn import decomposition
from sklearn.model_selection import cross_val_score
import time # computation time benchmark
from sklearn.discriminant_analysis import LinearDiscriminantAnalysis as LDA
from sklearn import neighbors
from sklearn.decomposition import KernelPCA
from sklearn.utils import shuffle

train = pd.read_csv("./input/data.csv")

answers = train["label"]
data = train.drop(labels=["label"], axis=1)


X, y = np.float32(data[:42000]) / 255., np.float32(answers[:42000])
X, y = shuffle(X, y)

# keeping just 15k training samples due to kPCA memory requirements
X_train, y_train = np.float32(X[:10000])/255., np.float32(y[:10000])
X_test, y_test = np.float32(X[37000:])/ 255., np.float32(y[37000:])

# uncomment for without PCA
X_lda = X_train

# uncomment for PCA
kpca = KernelPCA(kernel="rbf",n_components=400, gamma=1)
X_kpca = kpca.fit_transform(X_train)
X_test = kpca.transform(X_test)

# lda for dimensionality reduction.
lda = LDA()
X_lda = lda.fit_transform(X_kpca,y_train)
X_test = lda.transform(X_test)

# kNN classification
start = int(round(time.time() * 1000))
clf = neighbors.KNeighborsClassifier(n_neighbors=5)
clf.fit(X_lda, y_train)

end = int(round(time.time() * 1000))
print("--Treniravimos laikas: ", (end-start), "ms--------------")

expected = y_test
predicted = clf.predict(X_test)

print("--------------------Results-------------------")
print("Rezultatai \n%s\n"
     % (metrics.classification_report(expected, predicted)))
# print("Confusion matrix:\n%s" % metrics.confusion_matrix(expected, predicted))