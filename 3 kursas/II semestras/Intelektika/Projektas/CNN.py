import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.metrics import confusion_matrix
import itertools

from keras.utils.np_utils import to_categorical #one-hot-encoding
from keras.models import Sequential
from keras.layers import Dense, Dropout, Flatten, Conv2D, MaxPool2D
from keras.optimizers import RMSprop
from keras.preprocessing.image import ImageDataGenerator
from keras.callbacks import ReduceLROnPlateau

def plot_confusion_matrix(cm, classes,
                          normalize=False,
                          title='Confusion matrix',
                          cmap=plt.cm.Blues):
    plt.imshow(cm, interpolation='nearest', cmap=cmap)
    plt.title(title)
    plt.colorbar()
    tick_marks = np.arange(len(classes))
    plt.xticks(tick_marks, classes, rotation=45)
    plt.yticks(tick_marks, classes)

    if normalize:
        cm = cm.astype('float') / cm.sum(axis=1)[:, np.newaxis]

    thresh = cm.max() / 2.
    for i, j in itertools.product(range(cm.shape[0]), range(cm.shape[1])):
        plt.text(j, i, cm[i, j],
                 horizontalalignment="center",
                 color="white" if cm[i, j] > thresh else "black")

    plt.tight_layout()
    plt.ylabel('True label')
    plt.xlabel('Predicted label')


train = pd.read_csv("./input/data.csv")

answers = train["label"]
data = train.drop(labels=["label"], axis=1)

figureCount = 0

# rodyti duomenu variacijas
# sns.countplot(answers)
# plt.figure(figureCount)
# figureCount += 1

print(f"Ar yra sugadintu duomenu? {data.isnull().values.any()}")

# normalizuojam skaitines reiksmes
data = data / 255.0



# paverciam vektoriu skaitines reiksmes i 28x28 matricas
data = data.values.reshape(-1, 28, 28, 1)

# rodyti pavyzdini treniravimos paveiksliuka
# plt.imshow(data[3][:, :, 0])
# plt.title("Pavyzdiniai duomenys")
# plt.figure(figureCount)
# figureCount += 1

# nustatom iseiciu kieki (10)
answers = to_categorical(answers, num_classes=10)

total_pictures = 42000
epochs = 1
batch_size = 86

# Sukuriamas CNN modelis

model = Sequential()

model.add(Conv2D(filters=32, kernel_size=(5, 5), padding='Same',
                 activation='relu', input_shape=(28, 28, 1)))
model.add(Conv2D(filters=32, kernel_size=(5, 5), padding='Same',
                 activation='relu'))
model.add(MaxPool2D(pool_size=(2, 2)))
model.add(Dropout(0.25))


model.add(Conv2D(filters=64, kernel_size=(3, 3), padding='Same',
                 activation='relu'))
model.add(Conv2D(filters=64, kernel_size=(3, 3), padding='Same',
                 activation='relu'))
model.add(MaxPool2D(pool_size=(2, 2), strides=(2, 2)))
model.add(Dropout(0.25))


model.add(Flatten())
model.add(Dense(256, activation="relu"))
model.add(Dropout(0.5))
model.add(Dense(10, activation="softmax"))

rmsprop = RMSprop(lr=0.001, rho=0.9, epsilon=1e-08, decay=0.0)

model.compile(optimizer=rmsprop, loss="categorical_crossentropy", metrics=["accuracy"])

lr_reduction = ReduceLROnPlateau(monitor='val_acc',
                                            patience=2,
                                            verbose=1,
                                            factor=0.5,
                                            min_lr=0.0001)

datagen = ImageDataGenerator(
        featurewise_center=False,  # set input mean to 0 over the dataset
        samplewise_center=False,  # set each sample mean to 0
        featurewise_std_normalization=False,  # divide inputs by std of the dataset
        samplewise_std_normalization=False,  # divide each input by its std
        zca_whitening=False,  # apply ZCA whitening
        rotation_range=10,  # randomly rotate images in the range (degrees, 0 to 180)
        zoom_range=0.1,  # Randomly zoom image
        width_shift_range=0.1,  # randomly shift images horizontally (fraction of total width)
        height_shift_range=0.1,  # randomly shift images vertically (fraction of total height)
        horizontal_flip=False,  # randomly flip images
        vertical_flip=False)  # randomly flip images

iterations = 1
# Kryzmine patikra po 10% duomenu testavimui, likusieji 90% - mokymuisi
for i in range(iterations):
    kryz_train_data = np.delete(data, np.s_[int(i * total_pictures / iterations):int((i + 1) * total_pictures / iterations)], 0)
    kryz_train_answers = np.delete(answers, np.s_[int(i * total_pictures / iterations):int((i + 1) * total_pictures / iterations)], 0)

    kryz_test_data = data[int(i * total_pictures / iterations): int((i + 1) * total_pictures / iterations)]
    kryz_test_answers = answers[int(i * total_pictures / iterations): int((i + 1) * total_pictures / iterations)]

    datagen.fit(kryz_train_data)
    history = model.fit_generator(datagen.flow(data, answers, batch_size=batch_size),
                                  epochs=epochs, validation_data=(kryz_test_data, kryz_test_answers),
                                  verbose=2, steps_per_epoch=data.shape[0] // batch_size
                                  , callbacks=[lr_reduction])
    # Predict the values from the validation dataset
    Y_pred = model.predict(kryz_test_data)
    # Convert predictions classes to one hot vectors
    Y_pred_classes = np.argmax(Y_pred, axis=1)
    # Convert validation observations to one hot vectors
    Y_true = np.argmax(kryz_test_answers, axis=1)
    # compute the confusion matrix
    confusion_mtx = confusion_matrix(Y_true, Y_pred_classes)
    # plot the confusion matrix
    plot_confusion_matrix(confusion_mtx, classes=range(10))
    plt.figure(figureCount)
    figureCount += 1

plt.show()
