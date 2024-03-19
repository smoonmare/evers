# Use the official Node.js image as the base image
FROM node:20.11.1

# Set the working directory in the container
WORKDIR /app

# Copy the package.json and package-lock.json (if available)
COPY package*.json /app/

# Install project dependencies
RUN npm install

# Copy the project files into the working directory
COPY . /app

# Expose the port the app runs on
EXPOSE 4200

# Start the Angular development server
CMD ["npm", "start"]
